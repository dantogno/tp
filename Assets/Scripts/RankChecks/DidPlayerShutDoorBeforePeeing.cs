using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidPlayerShutDoorBeforePeeing : MonoBehaviour
{
    private bool hasPlayerPeed = false;
    private void OnPlayerStartedPeeing()
    {
        Debug.Log($"Rank: {Ranking.RankPoints}");
        if (!hasPlayerPeed)
        {
            hasPlayerPeed = true;
            if (!DoorStatusChecker.Instance.IsDoorOpen)
                Ranking.RankPoints++;
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
