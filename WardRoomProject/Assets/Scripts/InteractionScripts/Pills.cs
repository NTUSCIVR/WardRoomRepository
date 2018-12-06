using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using cakeslice;

public class Pills : MonoBehaviour {
    [SerializeField]
    VRTK_InteractableObject linkedObject;
    [SerializeField]
    private GameObject PillsObject;
    [SerializeField]
    private Transform PillsStartingTransform;
    [SerializeField]
    private BoxCollider PillsCollider;
    [SerializeField]
    private Outline PillOutline;
    [SerializeField]
    private Outline Pill2Outline;
    [SerializeField]
    ScriptableEvent m_event;

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched += InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched += InteractableObjectUnTouched;
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
        }

    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectTouched -= InteractableObjectTouched;
            linkedObject.InteractableObjectUntouched -= InteractableObjectUnTouched;
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
        }
    }

    protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
    {
        PillOutline.enabled = true;
        Pill2Outline.enabled = true;
    }

    protected virtual void InteractableObjectUnTouched(object sender, InteractableObjectEventArgs e)
    {
        PillOutline.enabled = false;
        Pill2Outline.enabled = false;
    }

    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (PillsCollider != null)
            PillsCollider.isTrigger = true;
        PillOutline.enabled = false;
        Pill2Outline.enabled = false;
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        gameObject.SetActive(false);

        m_event.Raise();
    }
}
