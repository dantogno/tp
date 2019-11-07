using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private Animator gateAnimator;

    [SerializeField]
    private Transform playerCameraTransform;

    [Tooltip("How far to raycast to check if the player is looking at the hitbox")]
    [SerializeField]
    private float maxRaycastDistance = 8;

    [Tooltip("Layermask to use for raycast.")]
    [SerializeField]
    private LayerMask layermask;

    private bool isDemonSummoned = false;
    private bool isDoorShut = false;
    private int shutDoorTrigger = Animator.StringToHash("ShutDoor");
    private RaycastHit raycastHit;

    private void FixedUpdate()
    {
        if (isDemonSummoned && !isDoorShut)
        {
            // raycast from camera to door look hitbox to detect if player is looking at door
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, maxRaycastDistance, layermask))
            {
                gateAnimator.SetTrigger(shutDoorTrigger);
            }
        }
    }
    private void OnDemonSummoned()
    {
        isDemonSummoned = true;
    }
    private void OnEnable()
    {
        SummonDemon.DemonSummoned += OnDemonSummoned;
    }
    private void OnDisable()
    {
        SummonDemon.DemonSummoned -= OnDemonSummoned;
    }

}
