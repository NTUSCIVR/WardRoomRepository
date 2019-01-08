using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ParticiapantData/Holder")]
public class PatientDataHolder : ScriptableObject {
    [SerializeField]
    PatientData m_data;

    public PatientData Data
    {
        get
        {
            return m_data;
        }

        set
        {
            m_data = value;
        }
    }
}
