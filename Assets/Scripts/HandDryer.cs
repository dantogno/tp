using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDryer : MonoBehaviour
{
    [SerializeField]
    private float triggerValue = 0.4f;

    [SerializeField]
    private float initialDisableTime = 1;

    [SerializeField]
    private Collider leftHandCollider, rightHandCollider;

    private Collider handDryingTrigger;
    private float timeObjectStarted;
    private AudioSource audioSource;
    private bool isLeftHandDry, isRightHandDry;
    public static HandDryer Instance;
    public bool AreHandsDry => !HandWashDetector.Instance.AreHandsWashed || (isLeftHandDry && isRightHandDry);
    
    void Start()
    {
        Instance = this;
        handDryingTrigger = GetComponent<Collider>();
        handDryingTrigger.enabled = false;
        audioSource = GetComponent<AudioSource>();
        timeObjectStarted = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == leftHandCollider)
            isLeftHandDry = true;
        else if (other == rightHandCollider)
            isRightHandDry = true;
        Debug.Log($"Left hand dry: {isLeftHandDry}. Right hand dry: {isRightHandDry}");

    }
    public void OnValueChanged(float value)
    {
        // Debug.Log($"Value: {value}");
        if (value < triggerValue && !audioSource.isPlaying && Time.time - timeObjectStarted > initialDisableTime)
        {
            audioSource.Play();
            handDryingTrigger.enabled = true;
            StartCoroutine(DisableDryingTriggerAfterAudioFinishes());
        }
    }
    private IEnumerator DisableDryingTriggerAfterAudioFinishes()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        handDryingTrigger.enabled = false;
    }
}
