using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Visual;

public class PortalWorldFader : MonoBehaviour
{
    [Tooltip("Used to for checking collision with the trigger.")]
    [SerializeField]
    private GameObject playerHeadObject;

    [Tooltip("Reference to the object that fades out the world when the player looks into collision.")]
    [SerializeField]
    private CameraColorOverlay cameraColorOverlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerHeadObject)
            cameraColorOverlay.AddColorOverlay();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerHeadObject)
            cameraColorOverlay.RemoveColorOverlay();
    }
}
