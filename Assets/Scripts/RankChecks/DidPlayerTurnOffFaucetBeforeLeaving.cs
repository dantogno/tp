using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidPlayerTurnOffFaucetBeforeLeaving : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    private bool isFaucetOn;

    private void Awake()
    {
        Ranking.TotalPossiblePoints += value;
    }

    private void OnLevelEnded()
    {
        if (!isFaucetOn)
        {
            Ranking.RankPoints += value;
            Debug.Log($"Awarded DidPlayerTurnOffFaucetBeforeLeaving rank value of {value}");
        }
    }
    private void OnFaucetTurnedOff()
    {
        isFaucetOn = false;
    }
    private void OnFaucetTurnedOn()
    {
        isFaucetOn = true;
    }
    private void OnEnable()
    {
        Faucet.FaucetTurnedOn += OnFaucetTurnedOn;
        Faucet.FaucetTurnedOff += OnFaucetTurnedOff;
        EndLevelTrigger.LevelEnded += OnLevelEnded;
    }
    private void OnDisable()
    {
        Faucet.FaucetTurnedOn -= OnFaucetTurnedOn;
        Faucet.FaucetTurnedOff -= OnFaucetTurnedOff;
        EndLevelTrigger.LevelEnded -= OnLevelEnded;
    }
}
