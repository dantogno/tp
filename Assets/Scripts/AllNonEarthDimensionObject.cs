using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllNonEarthDimensionObject : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToControl;

    private void OnPortalEntered(DimensionType dimension)
    {
        objectToControl.SetActive(true);   
    }
    private void OnPortalExited(DimensionType dimension)
    {
        objectToControl.SetActive(false);
    }
    private void OnEnable()
    {
        PortalInnerWorldTrigger.PortalEntered += OnPortalEntered;
        PortalInnerWorldTrigger.PortalExited += OnPortalExited;
    }
    private void OnDisable()
    {
        PortalInnerWorldTrigger.PortalEntered -= OnPortalEntered;
        PortalInnerWorldTrigger.PortalExited -= OnPortalExited;
    }
}
