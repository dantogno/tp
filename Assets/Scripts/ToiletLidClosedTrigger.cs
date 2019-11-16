using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidClosedTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public static event Action ToiletLidStartedOpening, ToiletLidFullyClosed;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ToiletClosed");
        ToiletLidFullyClosed?.Invoke();
        audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Started opening toilet lid!");
        ToiletLidStartedOpening?.Invoke();
    }
}
