using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidClosedTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Collider toiletLidCollider;

    public static event Action ToiletLidStartedOpening, ToiletLidFullyClosed;
    private void OnTriggerEnter(Collider other)
    {
        if (other == toiletLidCollider)
        {
            // Debug.Log("ToiletClosed");
            ToiletLidFullyClosed?.Invoke();
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == toiletLidCollider)
        {
            // Debug.Log("Started opening toilet lid!");
            ToiletLidStartedOpening?.Invoke();
        }
    }
}
