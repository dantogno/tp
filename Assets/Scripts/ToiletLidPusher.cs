using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidPusher : MonoBehaviour
{
    [SerializeField]
    private Rigidbody toiletLidRigidbody;

    [SerializeField]
    private float forceMultiplier = 2.0f;

    [SerializeField]
    private float closedRotationEulerAngles = 83.0f;

    [SerializeField]
    private float closedThreshold = 0.1f;

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("PeeButton"))
    //    {
    //        PushLidClosed();
    //    }
    //}

    private void FixedUpdate()
    {
        // useGravity helps the lid fall more naturally, but it must be disabled when the lid closes
        // to avoid jittery behavior. I'm turing it off early to help prevent it from closing too far,
        // beyond the limit of the RotationalJointDrive component, which can happen if it gathers too much speed.
        if (toiletLidRigidbody.useGravity)
        {
            if (toiletLidRigidbody.transform.localEulerAngles.x > closedRotationEulerAngles - closedThreshold && 
                toiletLidRigidbody.transform.localEulerAngles.x < closedRotationEulerAngles + closedThreshold)
            {
                // Debug.Log("PositionReached");
                toiletLidRigidbody.useGravity = false;                
            }
            else
            {
                //Debug.Log(toiletLidRigidbody.transform.localEulerAngles.x);
            }
        }
    }

    public void PushLidClosed()
    {
        toiletLidRigidbody.useGravity = true;
        toiletLidRigidbody.AddForce(toiletLidRigidbody.transform.forward * forceMultiplier);
    }

}
