using cakeslice;
using UnityEngine;

//-------------------------------------------------------------------------
/*
 * This Script allows Pick up Interaction, a specific Interaction in Toilet Scene and Highlighting(Outline) of Interactable Objects.
 * 
 * Pick up: Records objects' original position -> Hair Trigger Down -> Pick up -> Hair Trigger Down Again -> Put back to recorded position
 * 
 * Specific: Hair Trigger Down when hovering over Tap Handler(When Water Particle System is Off) on Sink -> Play On Animation in TapHandler Animator Controller & Play Water Particle System
 *           Hair Trigger Down when hovering over Tap Handler(When Water Particle System is On) on Sink -> Play Off Animation in TapHandler Animator Controller & Stop Water Particle System
 * 
 * HighLight(Outline): Hover over and stay on the Interactable Object with controller -> Outline will be shown
 *                     Move controller away from Interactable Object -> Outline will be hidden
 * 
 * ***Note***
 * ***For Interactions***
 * Preparation: Have a Collider Component attached to the mesh object.
 * Usage: Make an Empty GameObject as a parent of the said mesh object, add this script as a component of the Empty GameObject.
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
    public class ToiletScene_Interaction : MonoBehaviour
    {
        private Vector3 oldPosition;
        private Quaternion oldRotation;

        private Animator TapAnimator;
        private ParticleSystem Water;
        private bool WaterRunning = false;

        private Outline outline;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        //-------------------------------------------------
        void Awake()
        {
            if (gameObject.name == "Tap")
            {
                TapAnimator = GetComponentInChildren<Animator>();
                Water = GetComponentInChildren<ParticleSystem>();
            }
            
            // Save our position/rotation so that we can restore it when we detach
            oldPosition = transform.position;
            oldRotation = transform.rotation;

            outline = GetComponentInChildren<Outline>();
        }
        
        //-------------------------------------------------
        // Called when a Hand starts hovering over this object
        //-------------------------------------------------
        private void OnHandHoverBegin(Hand hand)
        {
            outline.enabled = true;
        }
        
        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
            outline.enabled = false;
        }
        
        //-------------------------------------------------
        // Called every Update() while a Hand is hovering over this object
        //-------------------------------------------------
        private void HandHoverUpdate(Hand hand)
        {
            if (hand.GetStandardInteractionButtonDown())
            {
                // Specific Interaction
                // Play/Stop Tap Handle Animations & Water Particle System
                if(gameObject.name == "Tap")
                {
                    if(!WaterRunning)
                    {
                        if(!TapAnimator.GetCurrentAnimatorStateInfo(0).IsName("On"))
                        {
                            TapAnimator.Play("On");
                        }
                        Water.Play();
                    }
                    else
                    {
                        if (!TapAnimator.GetCurrentAnimatorStateInfo(0).IsName("Off"))
                        {
                            TapAnimator.Play("Off");
                        }
                        Water.Stop();
                    }
                    WaterRunning = !WaterRunning;
                }
                else
                {
                    // General Pick Up
                    if (hand.currentAttachedObject != gameObject)
                    {
                        // Attach this object to the hand
                        hand.AttachObject(gameObject, attachmentFlags);
                    }
                }
            }
        }

        //-------------------------------------------------
        // Called every Update() while this GameObject is attached to the hand
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand)
        {
            if (hand.GetStandardInteractionButtonDown())
            {
                if (hand.currentAttachedObject == gameObject)
                {
                    // Restore position/rotation
                    transform.position = oldPosition;
                    transform.rotation = oldRotation;

                    // Detach this object from the hand
                    hand.DetachObject(gameObject);
                }
            }
        }
    }
}
