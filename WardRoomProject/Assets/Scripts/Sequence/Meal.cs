using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Meal : MonoBehaviour {
    [SerializeField]
    GameObjectRuntimeSet m_foods;
    [SerializeField]
    ScriptableEvent m_event;

    bool isDone = false;
	// Update is called once per frame
	void Update () {
        if (!isDone && m_foods.m_list.Count <= 0)
        {
            m_event.Raise();
            isDone = true;
            enabled = false;
        }

    }
}
