using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DimensionType
{
    NoteDimension,
    JumpScareDimension,
    KeyDimension
}
public class PortalController : MonoBehaviour
{
    [SerializeField]
    private DimensionType startingDimension;

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

    [SerializeField]
    private List<Color> portalColors;

    [SerializeField]
    private List<MeshRenderer> portalMeshes;

    [SerializeField]
    private Light portalLight;

    /// <summary>
    /// The dimension that the portal will lead to.
    /// </summary>
    public static event Action<DimensionType> ActiveDimensionChanged;
    // TODO make this a property and call event in setter?
    private DimensionType activeDimension;
    private int dimensionIndex;
    private int colorIndex = 0;
    private bool isPortalSpawned = false;
    private bool isDemonFlushed;
    private bool isGateShut;
    private bool obtainedNote;
    private RaycastHit raycastHit;
    private List<Material> portalMaterials = new List<Material>();

    private void Awake()
    {
        foreach (var item in portalMeshes)
        {
            portalMaterials.Add(item.material);
            Debug.Log($"Material name: {item.material.name}");
        }
        portalColors.Add(portalLight.color);
        SetDimension(startingDimension);
    }

    private void FixedUpdate()
    {
        UpdatePortalIsPortalSpawned();
    }

    /// <summary>
    /// Handles spawning the portal at the correct time.
    /// </summary>
    private void UpdatePortalIsPortalSpawned()
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

    private void OnToiletFlushed()
    {
        // Change dimensions if portal is spawned
        if (isPortalSpawned)
        {
            ChangeDimensions();
        }
    }

    private void SetDimension(DimensionType newDimension)
    {
        activeDimension = newDimension;
        ActiveDimensionChanged?.Invoke(activeDimension);
    }
    private void ChangeDimensions()
    {
        colorIndex++;
        colorIndex = colorIndex >= portalColors.Count ? 0 : colorIndex;

        portalLight.color = portalColors[colorIndex];
        foreach (var item in portalMaterials)
        {
            item.SetColor("_TintColor", portalColors[colorIndex]);
        }
        // TODO: update the active dimension here!
        ActiveDimensionChanged?.Invoke(activeDimension);
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
        FlushController.ToiletFlushed += OnToiletFlushed;
        GateController.GateShut += OnGateShut;
        FlushDemon.DemonFlushed += OnDemonFlushed;
    }
    private void OnDisable()
    {
        FlushController.ToiletFlushed -= OnToiletFlushed;
        GateController.GateShut -= OnGateShut;
        FlushDemon.DemonFlushed -= OnDemonFlushed;
    }
}
