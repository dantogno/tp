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

    private float timeObjectStarted;
    private AudioSource audioSource;
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
        if (value >= onValue && Time.time - timeObjectStarted > initialDisableTime)
        {
            if (audioSource != null && !audioSource.isPlaying)
                audioSource.Play();

            if (!particleSystem.isPlaying)
                particleSystem.Play();
        }
        else if (value <= offValue)
        {
            particleSystem.Stop();
        }
    }
}
