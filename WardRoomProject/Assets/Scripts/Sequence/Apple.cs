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
    [SerializeField]
    AudioSource m_sound;

    protected virtual void OnEnable()
    {
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
        }

    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectGrabbed -= InteractableObjectGrabbed;
        }
    }

    protected virtual void InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        if (AppleCollider != null)
            AppleCollider.isTrigger = true;

    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        gameObject.SetActive(false);
        m_sound.Play();
        m_event.Raise();
    }
}
