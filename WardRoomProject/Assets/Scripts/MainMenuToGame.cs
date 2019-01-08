using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuToGame : MonoBehaviour {
    [SerializeField]
    PatientDataList m_list;

    [SerializeField]
    TMPro.TMP_InputField m_inputfield;

    [SerializeField]
    GameObject m_invalidid;

    public void CheckChangeScene()
    {
        string id = m_inputfield.text;
        PatientData pd = System.Array.Find(m_list.m_list, element => element.Id == id);
        if(pd != null)
        {
            PlayerPrefs.SetString("ID", pd.Id);
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ShowInvalidIdText());
        }
    }

    IEnumerator ShowInvalidIdText()
    {
        m_invalidid.SetActive(true);
        float timer = 0.0f;
        while (timer < 3.0f)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        m_invalidid.SetActive(false);
    }

}
