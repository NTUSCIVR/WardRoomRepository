using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UtensilsPicked : MonoBehaviour {
    [SerializeField]
    VRTK_InteractableObject m_fork;

    [SerializeField]
    VRTK_InteractableObject m_knife;

    [SerializeField]
    ScriptableEvent m_event;

    // Update is called once per frame
    void Update () {
		if(m_fork.IsGrabbed() && m_knife.IsGrabbed())
        {
            m_event.Raise();
            Destroy(gameObject);
        }
	}
}
