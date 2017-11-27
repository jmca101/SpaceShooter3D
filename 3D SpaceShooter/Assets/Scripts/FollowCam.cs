using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public Transform target;
    public Vector3 defaultDistance = new Vector3(0.0f, 3.0f, -13.0f);
    public float distanceDamp = 10.0f;
    public Vector3 velocity = Vector3.one;

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            Vector3 toPos = target.position + (target.rotation * defaultDistance);
            Vector3 curPos = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDamp);
            transform.position = curPos;
            transform.LookAt(target, target.up);
        }
        

    }
}
