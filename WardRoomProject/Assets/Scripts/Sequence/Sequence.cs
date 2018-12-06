using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour {

    [SerializeField]
    MonoBehaviour[] m_components;

    public void Activate()
    {
        foreach (MonoBehaviour m in m_components)
        {
            m.enabled = true;
        }
    }

    public void Deactivate()
    {
        foreach (MonoBehaviour m in m_components)
        {
            m.enabled = false;
        }
    }
}
