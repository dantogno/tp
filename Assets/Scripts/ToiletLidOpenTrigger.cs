using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLidOpenTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private Collider toiletLidCollider;

    [Tooltip("If we don't disable the audio for a bit, it will play the sfx when the player exits the portal.")]
    [SerializeField]
    private float disableAudioTimeOnPortalExit = 0.25f;

    public static event Action ToiletLidFullyOpened;
    private bool isOnCooldown = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other == toiletLidCollider)
        {
            // Debug.Log("Toilet lid is fully open.");
            if (!isOnCooldown)
            {
                ToiletLidFullyOpened?.Invoke();
                isOnCooldown = true;
            }
            audioSource.Play();
        }
    }
    private IEnumerator DisableSoundForSeconds(float seconds)
    {
        audioSource.enabled = false;
        yield return new WaitForSeconds(seconds);
        audioSource.enabled = true;
    }
    private void OnPortalExited(DimensionType dimensionType)
    {
        StartCoroutine(DisableSoundForSeconds(disableAudioTimeOnPortalExit));
    }
    private void OnToiletLidFullyClosed()
    {
        isOnCooldown = false;
    }
    private void OnEnable()
    {
        ToiletLidClosedTrigger.ToiletLidFullyClosed += OnToiletLidFullyClosed;
        PortalInnerWorldTrigger.PortalExited += OnPortalExited;
    }
    private void OnDisable()
    {
        ToiletLidClosedTrigger.ToiletLidFullyClosed -= OnToiletLidFullyClosed;
        PortalInnerWorldTrigger.PortalExited -= OnPortalExited;
    }
}
