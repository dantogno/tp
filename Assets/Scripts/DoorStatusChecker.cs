using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStatusChecker : MonoBehaviour
{
    [Tooltip("If door joint value is less than this, the door is considered open.")]
    [SerializeField]
    private float openThreshold = -3f;
    public static DoorStatusChecker Instance;

    public bool IsDoorOpen { get; private set; }
    private void Start()
    {
        Instance = this;
    }

    public void OnDoorValueChange(float value)
    {
        IsDoorOpen = value < openThreshold;
        // Debug.Log($"Door open value: {value}");
    }
}
