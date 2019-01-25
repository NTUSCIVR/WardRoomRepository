
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Utensils : MonoBehaviour {
    [SerializeField]
    VRTK_InteractableObject linkedObject;
    [SerializeField]
    GameObject objectToDisable;
    [SerializeField]
    BoxCollider collider;
    [SerializeField]
    AudioSource utensilssounds;
    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);
        collider = (collider == null ? GetComponent<BoxCollider>() : collider);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectGrabbed += InteractableObjectGrabbed;
            linkedObject.InteractableObjectSnappedToDropZone += InteractableObjectSnappedToDropZone;
        }

    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
            linkedObject.InteractableObjectSnappedToDropZone -= InteractableObjectSnappedToDropZone;
        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (objectToDisable != null)
            objectToDisable.SetActive(false);
    }

    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (collider != null)
            collider.isTrigger = true;

        utensilssounds.Play();
    }

    protected virtual void InteractableObjectSnappedToDropZone(object sender, InteractableObjectEventArgs e)
    {
        if (collider != null)
            collider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            objectToDisable = (objectToDisable == null ? other.gameObject : objectToDisable);

            utensilssounds.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectToDisable = null;

    }
}
