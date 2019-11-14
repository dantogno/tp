using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private Collider key;

    [SerializeField]
    private Animator gateAnimator;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip unlockClip, openClip;

    [SerializeField]
    private Transform playerCameraTransform;

    [Tooltip("Plug in the door here, not the gate. Used to stop player from opening door while gate is shut.")]
    [SerializeField]
    private RotationalDriveFacade doorRotationalDriveFacade;

    [Tooltip("How far to raycast to check if the player is looking at the hitbox")]
    [SerializeField]
    private float maxRaycastDistance = 8;

    [Tooltip("Layermask to use for raycast.")]
    [SerializeField]
    private LayerMask layermask;

    [Tooltip("Object player has to look at to trigger the gate shutting.")]
    [SerializeField]
    private GameObject lookAtHitBox;

    public static event Action GateShut;
    private bool isDemonSummoned = false;
    private bool isDoorShut = false;
    private int shutDoorTrigger = Animator.StringToHash("ShutDoor");
    private int unlockTrigger = Animator.StringToHash("Unlock");
    private RaycastHit raycastHit;

    public void PlayOpenClip()
    {
        audioSource.clip = openClip;
        audioSource.Play();
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other == key)
        {
            audioSource.clip = unlockClip;
            audioSource.Play();
            gateAnimator.SetTrigger(unlockTrigger);
        }
    }
    private void FixedUpdate()
    {
        if (isDemonSummoned && !isDoorShut)
        {
            // raycast from camera to door look hitbox to detect if player is looking at door
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, maxRaycastDistance, layermask))
            {
                if (raycastHit.collider.gameObject == lookAtHitBox)
                {
                    gateAnimator.SetTrigger(shutDoorTrigger);                
                    audioSource.Play();
                    doorRotationalDriveFacade.MoveToTargetValue = true;
                    isDoorShut = true;
                    GateShut?.Invoke();
                }
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
