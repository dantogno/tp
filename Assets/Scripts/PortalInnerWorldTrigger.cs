using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Tracking.Collision;
using Zinnia.Visual;

public class PortalInnerWorldTrigger : MonoBehaviour
{
    [Tooltip("Used to for checking collision with the trigger.")]
    [SerializeField]
    private GameObject playerHeadObject;

    [Tooltip("CollisionFader object VRTK uses to fade out world during collisions.")]
    [SerializeField]
    private CollisionTracker collisionFader;

    [SerializeField]
    private CameraColorOverlay cameraColorOverlay;

    [Tooltip("The collider used to fade the world out when the player bumps it while entering the portal.")]
    [SerializeField]
    private Collider colliderToTriggerFadeIntoPortalInnerWorld;

    [Tooltip("These objects turn off when the player enters the portal, and back on when they exit it.")]
    [SerializeField]
    List<GameObject> objectsToDisableOnTriggerEnter;

    [Tooltip("These objects turn on when the player enters the portal, and back off when they exit it.")]
    [SerializeField]
    List<GameObject> objectsToEnableOnTriggerEnter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerHeadObject)
        {
            foreach (var item in objectsToDisableOnTriggerEnter)
            {
                item.SetActive(false);
            }
            foreach (var item in objectsToEnableOnTriggerEnter)
            {
                item.SetActive(true);
            }

            //collisionFader.enabled = false;
            cameraColorOverlay.RemoveColorOverlay();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerHeadObject)
        {
            foreach (var item in objectsToEnableOnTriggerEnter)
            {
                item.SetActive(false);
            }
            foreach (var item in objectsToDisableOnTriggerEnter)
            {
                item.SetActive(true);
            }
        }
    }
}
