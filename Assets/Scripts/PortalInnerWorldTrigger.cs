using System;
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

    [Tooltip("These objects turn off when the player enters the portal, and back on when they exit it." +
        "In other words, the objects of the Earth dimension.")]
    [SerializeField]
    List<GameObject> objectsToDisableOnTriggerEnter;

    public static event Action<DimensionType> PortalEntered;
    public static event Action<DimensionType> PortalExited;
    private DimensionType activeDimension;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerHeadObject)
        {
            PortalEntered?.Invoke(activeDimension);
            foreach (var item in objectsToDisableOnTriggerEnter)
            {
                item.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerHeadObject)
        {
            PortalExited?.Invoke(activeDimension);
            foreach (var item in objectsToDisableOnTriggerEnter)
            {
                item.SetActive(true);
            }
        }
    }

    private void OnActiveDimensionChanged(DimensionType newDimension)
    {
        activeDimension = newDimension;
    }

    private void OnEnable()
    {
        PortalController.ActiveDimensionChanged += OnActiveDimensionChanged;
    }
    private void OnDisable()
    {
        PortalController.ActiveDimensionChanged -= OnActiveDimensionChanged;
    }
}
