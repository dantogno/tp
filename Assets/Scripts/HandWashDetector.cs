using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWashDetector : MonoBehaviour
{
    [SerializeField]
    private Collider leftHandCollider, rightHandCollider;

    public static HandWashDetector Instance;
    private Collider collider;
    private bool isLeftHandWashed;
    private bool isRightHandWashed;
    public bool AreHandsWashed => isLeftHandWashed && isRightHandWashed;
    private void Start()
    {
        Instance = this;
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }
    private void OnFaucetTurnedOn()
    {
        collider.enabled = true;
    }
    private void OnFaucetTurnedOff()
    {
        collider.enabled = false;
    }
    private void OnPlayerStartedPeeing()
    {
        isLeftHandWashed = false;
        isRightHandWashed = false;
    }
    private void OnToiletFlushed()
    {
        isLeftHandWashed = false;
        isRightHandWashed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == leftHandCollider)
            isLeftHandWashed = true;
        else if (other == rightHandCollider)
            isRightHandWashed = true;
        Debug.Log($"Left hand washed: {isLeftHandWashed}. Right hand washed: {isRightHandWashed}");
    }
    private void OnEnable()
    {
        FlushController.ToiletFlushed += OnToiletFlushed;
        PeeController.PlayerStartedPeeing += OnPlayerStartedPeeing;
        Faucet.FaucetTurnedOn += OnFaucetTurnedOn;
        Faucet.FaucetTurnedOff += OnFaucetTurnedOff;
    }
    private void OnDisable()
    {
        FlushController.ToiletFlushed -= OnToiletFlushed;
        PeeController.PlayerStartedPeeing -= OnPlayerStartedPeeing;
        Faucet.FaucetTurnedOn -= OnFaucetTurnedOn;
        Faucet.FaucetTurnedOff -= OnFaucetTurnedOff;
    }
}
