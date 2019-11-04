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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetButtonDown("PeeButton"))
        {
            toiletLidRigidbody.useGravity = true;
            toiletLidRigidbody.AddForce(toiletLidRigidbody.transform.forward * forceMultiplier);
        }
    }

    private void FixedUpdate()
    {
        if (toiletLidRigidbody.useGravity)
        {
            if (toiletLidRigidbody.transform.localEulerAngles.x > closedRotationEulerAngles - closedThreshold && 
                toiletLidRigidbody.transform.localEulerAngles.x < closedRotationEulerAngles + closedThreshold)
            {
                Debug.Log("PositionReached");
                toiletLidRigidbody.useGravity = false;                
            }
            else
                Debug.Log(toiletLidRigidbody.transform.localEulerAngles.x);
        }
    }

    private void Start()
    {
        
    }

}
