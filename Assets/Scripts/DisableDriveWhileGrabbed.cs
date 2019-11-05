using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class DisableDriveWhileGrabbed : MonoBehaviour
{
    [SerializeField]
    private RotationalDriveFacade rotationalDriveFacade;

    public void DisableDriveOnGrab()
    {
        rotationalDriveFacade.MoveToTargetValue = false;
    }
    public void EnableDriveOnRelease()
    {
        rotationalDriveFacade.MoveToTargetValue = true;
    }
}
