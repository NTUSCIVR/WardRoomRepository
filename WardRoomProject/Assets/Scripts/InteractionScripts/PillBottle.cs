using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using cakeslice;

public class PillBottle : MonoBehaviour {
    [SerializeField]
    VRTK_InteractableObject linkedObject;
    [SerializeField]
    private GameObject Cap;
    [SerializeField]
    private Transform CapOldTransform;
    [SerializeField]
    private Transform CapRestingTransform;
    [SerializeField]
    private Outline CapOutline;
    [SerializeField]
    private Outline BodyOutline;
    [SerializeField]
    private BoxCollider BodyCollider;
    [SerializeField]
    private GameObject Pills;
    [SerializeField]
    private Transform PillsStartingTransform;
    [SerializeField]
    private BoxCollider PillsCollider;
    [SerializeField]
    AudioSource m_sound;

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += InteractableObjectUnTouched;
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
        }

    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched -= InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched -= InteractableObjectUnTouched;
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
        }
    }

    protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        CapOutline.enabled = true;
        BodyOutline.enabled = true;
    }

    protected virtual void InteractableObjectUnTouched(object sender, InteractableObjectEventArgs e)
    {
        CapOutline.enabled = false;
        BodyOutline.enabled = false;
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        // Open Cap
        Cap.transform.position = CapRestingTransform.position;

        // Remove Highlight & Collider
        CapOutline.enabled = false;
        BodyOutline.enabled = false;
        BodyCollider.enabled = false;

        m_sound.Play();

        // Move Pills out of Bottle & Allow Pills to be able to hover over
        Pills.GetComponent<VRTK_InteractableObject>().enabled = true;
        Pills.transform.position = PillsStartingTransform.position;
        PillsCollider.enabled = true;
        enabled = false;
    }
}
