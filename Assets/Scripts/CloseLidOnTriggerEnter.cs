using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLidOnTriggerEnter : MonoBehaviour
{
    private ToiletLidPusher toiletLidPusher;
    private void Awake()
    {
        toiletLidPusher = FindObjectOfType<ToiletLidPusher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            toiletLidPusher.PushLidClosed();
            Debug.Log("Entered trigger!");
        }
    }
}
