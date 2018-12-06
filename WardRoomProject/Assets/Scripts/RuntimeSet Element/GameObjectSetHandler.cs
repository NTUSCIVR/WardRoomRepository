using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSetHandler : MonoBehaviour {
    [SerializeField]
    GameObjectRuntimeSet m_runtimeset;

    private void OnEnable()
    {
        m_runtimeset.Add(gameObject);
    }

    private void OnDisable()
    {
        m_runtimeset.Remove(gameObject);
    }

}
