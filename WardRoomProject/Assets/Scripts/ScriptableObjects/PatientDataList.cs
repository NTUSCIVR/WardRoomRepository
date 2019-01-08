using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("ParticiapantData/PatientList"))]
public class PatientDataList : ScriptableObject {
    public PatientData[] m_list;
}
