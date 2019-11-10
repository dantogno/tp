using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class FlushController : MonoBehaviour
{
    [SerializeField]
    private RotationalJointDrive rotationalJointDrive;

    [Tooltip("When the angle is greater than this, a flush is registered. (uses absolute value)")]
    [SerializeField]
    private float flushAngle = 25.0f;

    [Tooltip("When the angle is lower than this, the flush is reset no longer on cooldown (uses absolute value)")]
    [SerializeField]
    private float flushResetAngle = 3.0f;

    [SerializeField]
    private AudioSource audioSource;

    public static event Action ToiletFlushed;
    private bool isOnCooldown;

    public void OnValueChanged()
    {
        if (isOnCooldown)
        {
            if (Mathf.Abs(rotationalJointDrive.Value) <= flushResetAngle)
            {
                isOnCooldown = false;
                //Debug.Log("Flush reset!");
            }
        }
        else if (Mathf.Abs(rotationalJointDrive.Value) >= flushAngle)
        {
            //Debug.Log("Flush detected!");
            isOnCooldown = true;
            ToiletFlushed?.Invoke();
            audioSource.Play();
        }
    }
}
