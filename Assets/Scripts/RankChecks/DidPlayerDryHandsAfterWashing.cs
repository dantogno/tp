using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidPlayerDryHandsAfterWashing : MonoBehaviour
{
    [SerializeField]
    private int value = 1;
    private void Awake()
    {
        Ranking.TotalPossiblePoints += value;
    }
    private void OnLevelEnded()
    {
        if (HandDryer.Instance.AreHandsDry)
        {
            Ranking.RankPoints += value;
            Debug.Log($"Awarded DidPlayerDryHandsAfterWashing rank value of {value}");
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
