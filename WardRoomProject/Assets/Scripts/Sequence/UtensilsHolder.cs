using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UtensilsHolder : MonoBehaviour {
    [SerializeField]
    VRTK_SnapDropZone m_forkholder;

    [SerializeField]
    VRTK_SnapDropZone m_knifeholder;

    bool forkplaced;
    bool knifeplaced;

    [SerializeField]
    ScriptableEvent m_event;

    private void OnEnable()
    {
        m_forkholder.ObjectSnappedToDropZone += CheckForkSnap;
        m_knifeholder.ObjectSnappedToDropZone += CheckKnifeSnap;
        m_forkholder.ObjectUnsnappedFromDropZone += RemoveFork;
        m_knifeholder.ObjectUnsnappedFromDropZone += RemoveKnife;
    }

    private void OnDisable()
    {
        m_forkholder.ObjectSnappedToDropZone -= CheckForkSnap;
        m_knifeholder.ObjectSnappedToDropZone -= CheckKnifeSnap;
        m_forkholder.ObjectUnsnappedFromDropZone -= RemoveFork;
        m_knifeholder.ObjectUnsnappedFromDropZone -= RemoveKnife;
    }

    void CheckForkSnap(object sender,SnapDropZoneEventArgs e)
    {
        forkplaced = true;

        if (knifeplaced)
        {
            m_event.Raise();
        }
    }

    void CheckKnifeSnap(object sender, SnapDropZoneEventArgs e)
    {
        knifeplaced = true;

        if (forkplaced)
        {
            m_event.Raise();
        }
    }

    void RemoveFork(object sender, SnapDropZoneEventArgs e)
    {
        forkplaced = false;
    }

    void RemoveKnife(object sender, SnapDropZoneEventArgs e)
    {
        knifeplaced = false;
    }
}

