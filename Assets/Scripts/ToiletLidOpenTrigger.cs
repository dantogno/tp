using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidOpenTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Toilet lid is fully open.");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Started closing toilet lid!");
    }
}
