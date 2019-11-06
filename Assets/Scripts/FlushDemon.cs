using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class FlushDemon : MonoBehaviour
{
    [SerializeField]
    private ConstantForce constantForceToDisable;

    [SerializeField]
    private SpringJoint springJointToDisable;

    private void OnToiletFlushed()
    {
        constantForceToDisable.enabled = false;
        springJointToDisable.breakForce = 0.01f;
    }
    private void OnEnable()
    {
        FlushController.ToiletFlushed += OnToiletFlushed;
    }
    private void OnDisable()
    {
        FlushController.ToiletFlushed -= OnToiletFlushed;
    }
}
