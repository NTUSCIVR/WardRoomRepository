using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAudio : MonoBehaviour {

    [SerializeField]
    AudioSource m_audioSource;

    [SerializeField]
    ScriptableEvent m_event;

    bool check = false;

    private void Update()
    {
        if (check)
        {
            Check();
        }
        else if (m_audioSource.isPlaying)
        {
            check = true;
        }
    }

    private void Check()
    {
        if(!m_audioSource.isPlaying)
        {
            m_event.Raise();
            check = false;
        }
    }
}
