using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletValve : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    private float timeObjectStarted;
    private AudioSource audioSource;

    private void Start()
    {
        particleSystem.Stop();
    }
    public void OnGrabbed()
    {
        if (!particleSystem.isPlaying)
            particleSystem.Play();
    }
}
