using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidClosedTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ToiletClosed");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Started opening toilet lid!");   
    }
}
