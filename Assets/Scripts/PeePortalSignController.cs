using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeePortalSignController : MonoBehaviour
{
    [SerializeField]
    private GameObject peeSign, portalSign;

    private void OnGateShut()
    {
        peeSign.SetActive(false);
        portalSign.SetActive(true);
    }
    private void OnEnable()
    {
        GateController.GateShut += OnGateShut;
    }
    private void OnDisable()
    {
        GateController.GateShut -= OnGateShut;
    }
}
