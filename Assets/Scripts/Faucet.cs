using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{
    [SerializeField]
    private float onValue = 0.4f;

    [SerializeField]
    private float offValue = 0.1f;

    [SerializeField]
    private float initialDisableTime = 1;

    [SerializeField]
    private ParticleSystem particleSystem;

    public static event Action FaucetTurnedOn, FaucetTurnedOff;
    private float timeObjectStarted;
    private AudioSource audioSource;
    private bool isFaucetOn = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeObjectStarted = Time.time;
        particleSystem.Stop();
    }

    public void OnValueChanged(float value)
    {
        // Debug.Log($"Value: {value}");
        if (value >= onValue && Time.time - timeObjectStarted > initialDisableTime && !isFaucetOn)
        {
            isFaucetOn = true;
            if (audioSource != null && !audioSource.isPlaying)
                audioSource.Play();

            if (!particleSystem.isPlaying)
                particleSystem.Play();

            FaucetTurnedOn?.Invoke();
        }
        else if (value <= offValue && isFaucetOn)
        {
            isFaucetOn = false;
            particleSystem.Stop();
            FaucetTurnedOff?.Invoke();
        }
    }
}
