using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidClosedTrigger : MonoBehaviour
{
    public static event Action ToiletLidStartedOpening, ToiletLidFullyClosed;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ToiletClosed");
        ToiletLidFullyClosed?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Started opening toilet lid!");
        ToiletLidStartedOpening?.Invoke();
    }
}
