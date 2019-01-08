using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItSeez3D.AvatarSdk.Core;
using ItSeez3D.AvatarSdkSamples.Core;


public class BodyAttacher : MonoBehaviour {
    [SerializeField]
    GameObject head;

    public GameObject Head
    {
        get
        {
            return head;
        }

        set
        {
            head = value;
        }
    }

    public void Bind()
    {
        BodyAttachment ba = GetComponent<BodyAttachment>();
        ba.AttachHeadToBody(
            head);
        ba.RebuildBindpose();
    }
}
