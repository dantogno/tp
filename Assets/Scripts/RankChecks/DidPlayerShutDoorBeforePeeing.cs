using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidPlayerShutDoorBeforePeeing : MonoBehaviour
{
    [SerializeField]
    private int value = 1; 

    private bool hasPlayerPeed = false;
    private void Awake()
    {
        Ranking.TotalPossiblePoints += value;
    }
    private void OnPlayerStartedPeeing()
    {
        // Debug.Log($"Rank: {Ranking.RankPoints}");
        if (!hasPlayerPeed)
        {
            hasPlayerPeed = true;
            if (!DoorStatusChecker.Instance.IsDoorOpen)
            {
                Debug.Log($"Awarded DidPlayerShutDoorBeforePeeing rank value of {value}");
                Ranking.RankPoints += value;
            }
        }
    }

    private void OnEnable()
    {
        PeeController.PlayerStartedPeeing += OnPlayerStartedPeeing;
    }
    private void OnDisable()
    {
        PeeController.PlayerStartedPeeing -= OnPlayerStartedPeeing;
    }
}
