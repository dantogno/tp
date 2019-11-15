using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Visual;

public class EndLevelTrigger : MonoBehaviour
{
    private CameraColorOverlay cameraColorOverlay;
    private AudioSource audioSource;
    private bool isGateUnlocked;

    private void Awake()
    {
        cameraColorOverlay = GetComponent<CameraColorOverlay>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isGateUnlocked)
        {
            cameraColorOverlay.AddColorOverlay();
            audioSource.Play();
        }
    }
    private void OnGateUnlocked()
    {
        isGateUnlocked = true;
    }
    private void OnEnable()
    {
        GateController.GateUnlocked += OnGateUnlocked;
    }
    private void OnDisable()
    {
        GateController.GateUnlocked -= OnGateUnlocked;
    }
}
