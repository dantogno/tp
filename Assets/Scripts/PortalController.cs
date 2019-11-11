using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private GameObject portalGameObject;

    [SerializeField]
    private Transform playerCameraTransform;

    [Tooltip("How far to raycast to check if the player is looking at the hitbox")]
    [SerializeField]
    private float maxRaycastDistance = 8;

    [Tooltip("Layermask to use for raycast.")]
    [SerializeField]
    private LayerMask layermask;

    [Tooltip("Object player has to look at to trigger the gate shutting.")]
    [SerializeField]
    private GameObject lookAtHitBox;

    private bool isPortalSpawned;
    private bool isDemonFlushed;
    private bool isGateShut;
    private RaycastHit raycastHit;

    private void FixedUpdate()
    {
        if (!isPortalSpawned && isGateShut && isDemonFlushed)
        {
            // raycast from camera to door look hitbox to detect if player is looking at door
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, maxRaycastDistance, layermask))
            {
                if (raycastHit.collider.gameObject == lookAtHitBox)
                {
                    isPortalSpawned = true;
                    portalGameObject.SetActive(true);
                }
            }
        }
    }
    private void OnDemonFlushed()
    {
        isDemonFlushed = true;
    }
    private void OnGateShut()
    {
        isGateShut = true;
    }
    private void OnEnable()
    {
        GateController.GateShut += OnGateShut;
        FlushDemon.DemonFlushed += OnDemonFlushed;
    }
    private void OnDisable()
    {
        GateController.GateShut -= OnGateShut;
        FlushDemon.DemonFlushed -= OnDemonFlushed;
    }
}
