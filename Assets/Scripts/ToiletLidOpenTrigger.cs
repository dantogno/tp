using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidOpenTrigger : MonoBehaviour
{
    public static event Action ToiletLidFullyOpened;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Toilet lid is fully open.");
        ToiletLidFullyOpened?.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Started closing toilet lid!");
    }
}
