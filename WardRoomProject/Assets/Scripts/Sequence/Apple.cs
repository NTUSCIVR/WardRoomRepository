using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Apple : MonoBehaviour {

    [SerializeField]
    VRTK_InteractableObject linkedObject;
    [SerializeField]
    private GameObject AppleObject;
    [SerializeField]
    private BoxCollider AppleCollider;

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

    }

    protected virtual void InteractableObjectUnTouched(object sender, InteractableObjectEventArgs e)
    {

    }

    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (AppleCollider != null)
            AppleCollider.isTrigger = true;

    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        gameObject.SetActive(false);

        m_event.Raise();
    }
}
