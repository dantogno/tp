using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidPusher : MonoBehaviour
{
    [SerializeField]
    private Rigidbody toiletLidRigidbody;

    [SerializeField]
    private float forceMultiplier = 2.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            toiletLidRigidbody.AddForce(toiletLidRigidbody.transform.forward * forceMultiplier);
        }
    }

}
