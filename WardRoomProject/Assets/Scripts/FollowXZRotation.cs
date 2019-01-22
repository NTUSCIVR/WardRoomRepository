using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowXZRotation : MonoBehaviour {
    [SerializeField]
    Transform m_follow;

    [SerializeField]
    Transform m_child;

	// Update is called once per frame
	void Update () {
        float y = m_follow.eulerAngles.y;
        Vector3 newvec = transform.eulerAngles;
        newvec.y = y;
        transform.eulerAngles = newvec;
        
	}

    public void Setup()
    {
        m_child.eulerAngles = new Vector3(0, -90, 0);
    }
}
