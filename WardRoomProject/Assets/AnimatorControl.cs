using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControl : MonoBehaviour {
    [SerializeField]
    string Done;

    Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void SetBool(bool _val)
    {
        m_animator.SetBool(Done, _val);
    }
}
