using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour {
    [SerializeField]
    GameObjectRuntimeSet m_foods;
    [SerializeField]
    ScriptableEvent m_event;
	// Update is called once per frame
	void Update () {
        if (m_foods.m_list.Count <= 0)
        {
            m_event.Raise();
            enabled = false;
        }
    }
}
