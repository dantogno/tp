using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidOpenTrigger : MonoBehaviour
{
    public static event Action ToiletLidFullyOpened;
    private bool isOnCooldown = true;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Toilet lid is fully open.");
        if (!isOnCooldown)
        {
            ToiletLidFullyOpened?.Invoke();
            isOnCooldown = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Started closing toilet lid!");
    }
    private void OnToiletLidFullyClosed()
    {
        isOnCooldown = false;
    }
    private void OnEnable()
    {
        ToiletLidClosedTrigger.ToiletLidFullyClosed += OnToiletLidFullyClosed;
    }
    private void OnDisable()
    {
        ToiletLidClosedTrigger.ToiletLidFullyClosed -= OnToiletLidFullyClosed;
    }
}
