using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletValve : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    private float timeObjectStarted;
    private AudioSource audioSource;
    private bool isTriggered;

    private void OnEnable()
    {
       if (!isTriggered)
            particleSystem.Stop();
       else
            particleSystem.Play();
    }

    public void OnGrabbed()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            particleSystem.Play();
        }            
    }
}
