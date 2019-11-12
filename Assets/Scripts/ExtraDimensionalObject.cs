using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDimensionalObject : MonoBehaviour
{
    [SerializeField]
    private DimensionType dimensionObjectLivesIn;

    [Tooltip("If the player can bring this through the portal, check this box.")]
    [SerializeField]
    private bool canObjectChangeDimensions = false;

    [Tooltip("When the object is brought above this Y, it will become part of the Earth dimension.")]
    [SerializeField]
    private float portalYBoundary;

    [Tooltip("This script has to go on a dummy parent object so that it can stay active for event listening while the child is disabled." +
        "Plug in the child object here.")]
    [SerializeField]
    private GameObject childObjectToControl;

    private new Rigidbody rigidbody;
    private ConstantForce constantForce;
    /// <summary>
    /// True if object has been pulled through the portal into the earth dimension.
    /// </summary>
    private bool isInEarthDimension;

    private void Awake()
    {
        if (canObjectChangeDimensions)
        {
            rigidbody = childObjectToControl.GetComponent<Rigidbody>();
            constantForce = childObjectToControl.GetComponent<ConstantForce>();
        }
    }

    private void FixedUpdate()
    {
        if (canObjectChangeDimensions && childObjectToControl.activeSelf && !isInEarthDimension)
        {
            if (childObjectToControl.transform.position.y > portalYBoundary)
            {
                if (constantForce != null)
                    constantForce.enabled = false;
                if (rigidbody != null)
                    rigidbody.useGravity = true;

                isInEarthDimension = true;
            }
        }
    }

    private void OnPortalEntered(DimensionType activeDimension)
    {
        if (isInEarthDimension)
            childObjectToControl.SetActive(false);
        else
        {
            if (activeDimension == dimensionObjectLivesIn)
                childObjectToControl.SetActive(true);
        }
    }
    private void OnPortalExited(DimensionType activeDimension)
    {
        if (isInEarthDimension)
            childObjectToControl.SetActive(true);
        else
        {
            if (activeDimension == dimensionObjectLivesIn)
                childObjectToControl.SetActive(false);
        }

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
