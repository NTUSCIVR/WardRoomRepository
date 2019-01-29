using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ParticiapantData/Patient"))]
public class PatientData : ScriptableObject {
    [SerializeField]
    string id;
    [SerializeField]
    Mesh m_head;
    [SerializeField]
    Material m_headmaterial;
    [SerializeField]
    Mesh m_hair;
    [SerializeField]
    Material m_hairmaterial;
    [SerializeField]
    string m_name;
    [SerializeField]
    Sprite m_photo;
    int option;

    public string Id
    {
        get
        {
            return id;
        }
    }

    public Mesh Head
    {
        get
        {
            return m_head;
        }
    }

    public Material Headmaterial
    {
        get
        {
            return m_headmaterial;
        }
    }

    public Mesh Hair
    {
        get
        {
            return m_hair;
        }
    }

    public Material Hairmaterial
    {
        get
        {
            return m_hairmaterial;
        }
    }

    public string Name
    {
        get
        {
            return m_name;
        }
    }

    public int Option
    {
        get
        {
            return option;
        }

        set
        {
            option = value;
        }
    }

    public Sprite Photo
    {
        get
        {
            return m_photo;
        }

        set
        {
            m_photo = value;
        }
    }
}
