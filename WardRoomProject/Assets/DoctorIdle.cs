using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorIdle : StateMachineBehaviour {

    AudioSource[] m_audioSource;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_audioSource == null)
        {
            try
            {
                m_audioSource = animator.GetComponentsInChildren<AudioSource>();
            }
            catch
            {

            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (AudioSource AS in m_audioSource)
        {
            if (AS.isPlaying)
            {
                animator.SetBool("Talking", true);
                break;
            }
            else
            {
                animator.SetBool("Talking", false);
            }
        }
    }
}
