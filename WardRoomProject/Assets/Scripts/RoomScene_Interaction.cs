using cakeslice;
using UnityEngine;

//-------------------------------------------------------------------------
/*
 * This Script allows a few Interactions and Highlighting(Outline) of Interactable Objects.
 * 
 * Interactions:
 * Open Bottle: Hair Trigger Down(When Hovering over Bottle) -> Cap & Pills moved to beside Bottle
 * Take Pills: Hair Trigger Down(When Hovering over Pills) -> Pills attached to hand & Cap return to original position
 * Eat Pills: Hair Trigger Down(When Pills attached to hand) -> Pills disappeared(Destroyed)
 * 
 * HighLight(Outline): Hover over and stay on the Interactable Object with controller -> Outline will be shown
 *                     Move controller away from Interactable Object -> Outline will be hidden
 * 
 * ***Note***
 * ***For Interactions***
 * Usage: Drag MedBottle Prefab into scene.
 * 
 * ***Note***
 * ***For Highlight***
 * It's related to a plugin Imported from Unity Asset Store.
 * Related files are stored in Assets under OutlineEffect Folder.
 * 
 * Preparation: Have a Collider Component attached to the mesh object.
 * Usage: Add OutlineEffect Script Component onto Camera GameObject.
 *        Add Outline Script Component onto the mesh object mentioned in Preparation.
 *        Keep it(Outline Script Component) disabled, unless you prefer the objects to be highlighted at the start.
 *        Tweak with the variables in Inspector to suit your purpose/preference.
 * 
 * Extra Note: If you prefer auto enable of highlight on all objects with Outline Script Component, look into OutlineEffect Script, Un-Comment the codes in OnEnable().
 *             It will auto enable all the Outline Script Components in the scene.
 */
//-------------------------------------------------------------------------

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class RoomScene_Interaction : MonoBehaviour
    {
        private GameObject Cap;
        private Transform CapOldTransform;
        private Transform CapRestingTransform;
        private Outline CapOutline;

        private Outline BodyOutline;
        private BoxCollider BodyCollider;

        private GameObject Pills;
        private Transform PillsStartingTransform;
        private Outline PillOutline;
        private Outline Pill2Outline;
        private BoxCollider PillsCollider;

        private bool CapOpened = false;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        //-------------------------------------------------
        void Awake()
        {
            Cap = transform.Find("Cap").gameObject;
            CapOldTransform = transform.Find("Cap Old");
            CapRestingTransform = transform.Find("Cap Resting");
            CapOutline = Cap.GetComponent<Outline>();

            GameObject Body = transform.Find("Body").gameObject;
            BodyOutline = Body.GetComponent<Outline>();
            BodyCollider = Body.GetComponent<BoxCollider>();

            Pills = transform.Find("Pills").gameObject;
            PillsStartingTransform = transform.Find("Pills Starting Position");
            PillsCollider = Pills.GetComponent<BoxCollider>();
            GameObject Pill_1 = Pills.transform.Find("Pill 1").gameObject;
            PillOutline = Pill_1.GetComponent<Outline>();
            GameObject Pill_2 = Pills.transform.Find("Pill 2").gameObject;
            Pill2Outline = Pill_2.GetComponent<Outline>();
        }
        
        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            if (!CapOpened)
            {
                CapOutline.enabled = true;
                BodyOutline.enabled = true;
            }
            else
            {
                PillOutline.enabled = true;
                Pill2Outline.enabled = true;
            }
        }
        
        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
            if (!CapOpened)
            {
                CapOutline.enabled = false;
                BodyOutline.enabled = false;
            }
            else
            {
                // Don't do anything if there is no pills
                if (!Pills)
                    return;

                PillOutline.enabled = false;
                Pill2Outline.enabled = false;
            }
        }
        
        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            if (hand.GetStandardInteractionButtonDown())
            {
                if (!CapOpened)
                {
                    // Open Cap
                    Cap.transform.position = CapRestingTransform.position;
                    CapOpened = true;

                    // Remove Highlight & Collider
                    CapOutline.enabled = false;
                    BodyOutline.enabled = false;
                    BodyCollider.enabled = false;

                    // Move Pills out of Bottle & Allow Pills to be able to hover over
                    Pills.transform.position = PillsStartingTransform.position;
                    PillsCollider.enabled = true;
                }
                else
                {
                    if (!Pills)
                        return;

                    // Pick Up
                    if (hand.currentAttachedObject != Pills)
                    {
                        // Call this to continue receiving HandHoverUpdate messages,
                        // and prevent the hand from hovering over anything else
                        hand.HoverLock(GetComponent<Interactable>());

                        // Attach this object to the hand
                        hand.AttachObject(Pills, attachmentFlags);

                        // Remove Highlight
                        PillOutline.enabled = false;
                        Pill2Outline.enabled = false;

                        // Close Cap
                        Cap.transform.position = CapOldTransform.position;
                    }
                    else
                    {
                        // Detach this object from the hand
                        hand.DetachObject(Pills);

                        // Call this to undo HoverLock
                        hand.HoverUnlock(GetComponent<Interactable>());

                        // Eat Pill(Destroy it)
                        Destroy(Pills);
                    }
                }
            }
        }
    }
}
