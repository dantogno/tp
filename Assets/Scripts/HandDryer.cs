using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDryer : MonoBehaviour
{
    [SerializeField]
    private float triggerValue = 0.4f;

    [SerializeField]
    private float initialDisableTime = 1;

    private float timeObjectStarted;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeObjectStarted = Time.time;
    }

    public void PlayAudioOnValueChanged(float value)
    {
        // Debug.Log($"Value: {value}");
        if (value < triggerValue && !audioSource.isPlaying && Time.time - timeObjectStarted > initialDisableTime)
            audioSource.Play();
    }
}
