﻿using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class MyInteraction : MonoBehaviour
    {
        [SerializeField]
        private Vector3 SinkHolePosition;
        [SerializeField]
        private Camera Eyes;

        private Vector3 oldPosition;
        private Quaternion oldRotation;
        private Animator TapAnimator;
        private ParticleSystem Water;
        private bool WaterRunning = false;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

        //-------------------------------------------------
        void Awake()
        {
            if (gameObject.name == "Tap")
            {
                TapAnimator = GetComponentInChildren<Animator>();
                Water = GetComponentInChildren<ParticleSystem>();
            }

            if (gameObject.name != "Pill")
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
        }


        //-------------------------------------------------
        // Called when a Hand stops hovering over this object
        //-------------------------------------------------
        private void OnHandHoverEnd(Hand hand)
        {
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
                        if (gameObject.name == "Pill")
                        {
                            GameObject Pill = transform.GetChild(0).gameObject;
                            Pill.GetComponent<Rigidbody>().isKinematic = true;
                            Pill.GetComponent<Rigidbody>().useGravity = false;
                        }

                        // Attach this object to the hand
                        hand.AttachObject(gameObject, attachmentFlags);
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
            if (hand.GetStandardInteractionButtonDown())
            {
                if (hand.currentAttachedObject == gameObject)
                {
                    if (gameObject.name == "Pill")
                    {
                        GameObject Pill = transform.GetChild(0).gameObject;
                        Pill.GetComponent<Rigidbody>().isKinematic = false;
                        Pill.GetComponent<Rigidbody>().useGravity = true;
                    }
                    else
                    {
                        // Restore position/rotation
                        transform.position = oldPosition;
                        transform.rotation = oldRotation;
                    }

                    // Detach this object from the hand
                    hand.DetachObject(gameObject);
                }
            }
            // Detect if Reach near mouth, true => kill pill
            else
            {
                if (gameObject.name == "Pill")
                {
                    if (hand.currentAttachedObject == gameObject)
                    {
                        float Distance = Vector3.Distance(gameObject.transform.position, Eyes.transform.position);
                        if (Distance <= 0.25f)
                        {
                            hand.DetachObject(gameObject);
                            Destroy(gameObject);
                        }
                    }
                }
            }
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
