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
    private List<MeshRenderer> portalMeshes;

    [SerializeField]
    private Light portalLight;

    [SerializeField]
    private Color noteDimensionColor, jumpScareDimensionColor, keyDimensionColor;

    /// <summary>
    /// The dimension that the portal will lead to.
    /// </summary>
    public static event Action<DimensionType> ActiveDimensionChanged;
    public static event Action PortalSpawned;
    // TODO make this a property and call event in setter?
    private DimensionType activeDimension;
    private bool IsPortalSpawned => portalGameObject.activeSelf;
    private bool isDemonFlushed;
    private bool isGateShut;
    private RaycastHit raycastHit;
    private List<Material> portalMaterials = new List<Material>();

    private void Awake()
    {
        foreach (var item in portalMeshes)
        {
            portalMaterials.Add(item.material);
            Debug.Log($"Material name: {item.material.name}");
        }
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
        if (!IsPortalSpawned && isGateShut && isDemonFlushed)
        {
            // raycast from camera to door look hitbox to detect if player is looking at door
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, maxRaycastDistance, layermask))
            {
                if (raycastHit.collider.gameObject == lookAtHitBox)
                {
                    portalGameObject.SetActive(true);
                    PortalSpawned?.Invoke();
                }
            }
        }
    }

    private void OnToiletFlushed()
    {
        // Change dimensions if portal is spawned
        if (IsPortalSpawned)
        {
            CycleDimensions();
        }
    }

    private void SetDimension(DimensionType newDimension)
    {
        activeDimension = newDimension;
        UpdatePortalColor();
        ActiveDimensionChanged?.Invoke(activeDimension);
    }
    private void CycleDimensions()
    {
        switch (activeDimension)
        {
            case DimensionType.NoteDimension:
                activeDimension = DimensionType.JumpScareDimension;
                break;
            case DimensionType.JumpScareDimension:
                activeDimension = DimensionType.KeyDimension;
                break;
            case DimensionType.KeyDimension:
                activeDimension = DimensionType.NoteDimension;
                break;
            default:
                break;
        }
        UpdatePortalColor();
        ActiveDimensionChanged?.Invoke(activeDimension);
    }

    private void UpdatePortalColor()
    {
        Color colorToSet;
        switch (activeDimension)
        {
            case DimensionType.NoteDimension:
                colorToSet = noteDimensionColor;
                break;
            case DimensionType.JumpScareDimension:
                colorToSet = jumpScareDimensionColor;
                break;
            case DimensionType.KeyDimension:
                colorToSet = keyDimensionColor;
                break;
            default:
                colorToSet = noteDimensionColor;
                break;
        }

        portalLight.color = colorToSet;
        foreach (var item in portalMaterials)
        {
            item.SetColor("_TintColor", colorToSet);
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
