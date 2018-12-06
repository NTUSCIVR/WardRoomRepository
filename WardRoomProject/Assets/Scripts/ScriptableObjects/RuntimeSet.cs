using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSet<T> : ScriptableObject {

    public List<T> m_list = new List<T>();

    public void Add(T _object)
    {
        if (m_list.Contains(_object))
            return;

        m_list.Add(_object);

    }

    public void Remove(T _object)
    {
        if (!m_list.Contains(_object))
            return;

        m_list.Remove(_object);
    }
}
