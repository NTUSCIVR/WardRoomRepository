using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour {

    [SerializeField]
    MonoBehaviour[] m_components;

    [SerializeField]
    GameObject[] m_gameObjects;

    public void Activate()
    {
        foreach (MonoBehaviour m in m_components)
        {
            m.enabled = true;
        }

        foreach (GameObject go in m_gameObjects)
        {
            go.SetActive(true);
        }
    }

    public void Deactivate()
    {
        foreach (MonoBehaviour m in m_components)
        {
            m.enabled = false;
        }

        foreach (GameObject go in m_gameObjects)
        {
            go.SetActive(false);
        }
    }
}
