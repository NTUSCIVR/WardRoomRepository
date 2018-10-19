using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class MedInteraction : MonoBehaviour
    {
        [SerializeField]
        private Material HoverHighlight_Material;
        [SerializeField]
        private Material Cap_Material;
        [SerializeField]
        private Material BottleMaterial;

        private Vector3 oldPosition;
        private Quaternion oldRotation;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        //-------------------------------------------------
        void Awake()
        {
            if (gameObject.name == "MedBottle")
            {
                // Save our position/rotation so that we can restore it when we detach
                oldPosition = transform.position;
                oldRotation = transform.rotation;
            }
        }


        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            if (gameObject.name == "MedBottle")
                Debug.Log("Enter Bottle");
            else if(gameObject.name == "Cap")
            {
                if (hand.otherHand.currentAttachedObject != null &&
                    hand.otherHand.currentAttachedObject != gameObject)
                {
                    Debug.Log("Enter Cap");
                    GameObject CapMesh = transform.GetChild(0).gameObject;
                    CapMesh.GetComponent<MeshRenderer>().material = HoverHighlight_Material;
                }
            }
        }


        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
            if (gameObject.name == "Cap")
            {
                if (hand.otherHand.currentAttachedObject != null &&
                    hand.otherHand.currentAttachedObject != gameObject)
                {
                    GameObject CapMesh = transform.GetChild(0).gameObject;
                    CapMesh.GetComponent<MeshRenderer>().material = Cap_Material;
                }
            }
        }


        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            /* || ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))*/

            if (hand.GetStandardInteractionButtonDown())
            {
                // Specific Interaction
                if(gameObject.name == "Cap")
                {
                    // Open Cap
                    gameObject.SetActive(false);
                }
                else
                {
                    // Pick Up Bottle
                    if (hand.currentAttachedObject != gameObject)
                    {
                        // Call this to continue receiving HandHoverUpdate messages,
                        // and prevent the hand from hovering over anything else
                        hand.HoverLock(GetComponent<Interactable>());

                        // Attach this object to the hand
                        hand.AttachObject(gameObject, attachmentFlags);
                        GameObject BottleMesh = transform.Find("Cylinder02").gameObject;
                        BottleMesh.GetComponent<MeshRenderer>().material = BottleMaterial;

                        GameObject Cap = transform.Find("Cap").gameObject;
                        if (Cap.GetComponent<MedInteraction>() == null)
                        {
                            MedInteraction CapScript = Cap.AddComponent<MedInteraction>();
                            CapScript.HoverHighlight_Material = this.HoverHighlight_Material;
                            CapScript.Cap_Material = this.Cap_Material;
                        }
                    }
                    // Drop Bottle
                    else
                    {
                        // Detach this object from the hand
                        hand.DetachObject(gameObject);

                        // Call this to undo HoverLock
                        hand.HoverUnlock(GetComponent<Interactable>());

                        // Restore position/rotation
                        transform.position = oldPosition;
                        transform.rotation = oldRotation;

                        // Close Cap
                        GameObject Cap = transform.Find("Cap").gameObject;
                        GameObject CapMesh = Cap.transform.GetChild(0).gameObject;
                        CapMesh.GetComponent<MeshRenderer>().material = Cap_Material;
                        Destroy(Cap.GetComponent<MedInteraction>());
                        Destroy(Cap.GetComponent<Interactable>());
                        Cap.SetActive(true);
                    }
                }
            }
        }


        //-------------------------------------------------
        // Called when this GameObject becomes attached to the hand
        //-------------------------------------------------
        private void OnAttachedToHand(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called when this GameObject is detached from the hand
        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called when this attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusAcquired(Hand hand)
        {
        }


        //-------------------------------------------------
        // Called when another attached GameObject becomes the primary attached object
        //-------------------------------------------------
        private void OnHandFocusLost(Hand hand)
        {
        }
    }
}
