using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {
    [SerializeField]
    ScriptableEvent m_event;

    private void OnEnable()
    {
        m_event.Raise();
    }
}
