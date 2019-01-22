using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Clipboard : MonoBehaviour {

    [SerializeField]
    VRTK_InteractableObject linkedObject;
    [SerializeField]
    ScriptableEvent m_event;
    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectGrabbed += InteractableObjectGrabbed;

        }

    }

    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
        linkedObject.InteractableObjectSnappedToDropZone += InteractableObjectSnappedToDropZone;
    }

    protected virtual void InteractableObjectSnappedToDropZone(object sender, InteractableObjectEventArgs e)
    {
        linkedObject.isGrabbable = false;
        m_event.Raise();
        linkedObject.InteractableObjectSnappedToDropZone -= InteractableObjectSnappedToDropZone;
    }
}
