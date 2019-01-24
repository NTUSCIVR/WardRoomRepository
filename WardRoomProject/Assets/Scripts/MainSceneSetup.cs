using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneSetup : MonoBehaviour {

    [SerializeField]
    PatientDataHolder m_holder;

    [SerializeField]
    PatientDataList m_list;

    [SerializeField]
    GameObject m_body;

    [SerializeField]
    TMPro.TextMeshPro m_textbox;

    private void Awake()
    {
        string id = PlayerPrefs.GetString("ID");
        m_holder.Data = System.Array.Find(m_list.m_list, element => element.Id == id);
        CreateModels();
        m_textbox.text = "Name: " + m_holder.Data.Name;
        Destroy(gameObject);
    }

    void CreateModels()
    {
        var avatarObject = new GameObject("ItSeez3D Avatar");// create head object in the scene
        var headObject = new GameObject("HeadObject");
        var headMeshRenderer = headObject.AddComponent<SkinnedMeshRenderer>();
        headMeshRenderer.sharedMesh = m_holder.Data.Head;
        headMeshRenderer.material = m_holder.Data.Headmaterial;
        headObject.transform.SetParent(avatarObject.transform);
        headObject.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;

        if (m_holder.Data.Hair != null)
        {
            var meshObject = new GameObject("HaircutObject");
            var meshRenderer = meshObject.AddComponent<SkinnedMeshRenderer>();
            meshRenderer.sharedMesh = m_holder.Data.Hair;
            meshRenderer.material = m_holder.Data.Hairmaterial;
            meshObject.transform.SetParent(avatarObject.transform);
            meshObject.GetComponent<SkinnedMeshRenderer>().updateWhenOffscreen = true;
        }
        avatarObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        if (m_body.GetComponentInChildren<BodyAttacher>())
        {
            //add head to the body
            m_body.GetComponentInChildren<BodyAttacher>().Head = avatarObject;
            m_body.GetComponentInChildren<BodyAttacher>().Bind();
        }
    }
}
