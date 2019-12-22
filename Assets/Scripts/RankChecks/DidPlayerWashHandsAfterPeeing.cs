using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidPlayerWashHandsAfterPeeing : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    private void Awake()
    {
        Ranking.TotalPossiblePoints += value;
    }
    private void OnLevelEnded()
    {
        if (HandWashDetector.Instance.AreHandsWashed)
        {
            Ranking.RankPoints += value;
            Debug.Log($"Awarded DidPlayerWashHandsAfterPeeing rank value of {value}");
        }
    }
    private void OnEnable()
    {
        EndLevelTrigger.LevelEnded += OnLevelEnded;
    }

    private void OnDisable()
    {
        EndLevelTrigger.LevelEnded -= OnLevelEnded;
    }
}
