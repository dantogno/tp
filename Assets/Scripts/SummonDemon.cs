using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonDemon : MonoBehaviour
{
    [SerializeField]
    private Animator demonAnimator;

    [SerializeField]
    [Tooltip("Player must fully open the lid this many times before the next" +
        "time they start opening the lid summons the demon.")]
    private int timesPlayerOpensLidBeforeSummoning = 1;

    public static event Action DemonSummoned;
    private int timesPlayerHasFullyOpenedLid = 0;
    private bool demonSummoned = false;
    private int summonAnimTrigger = Animator.StringToHash("Summon");

    private void OnToiletLidFullyOpened()
    {
        timesPlayerHasFullyOpenedLid++;
        Debug.Log($"Times Player has fully opened lid: {timesPlayerHasFullyOpenedLid}");
    }
    private void OnToiletLidStartedOpening()
    {
        Debug.Log("Received lid opening event!");
        Debug.Log($"Times Player has fully opened lid: {timesPlayerHasFullyOpenedLid}");
        if (timesPlayerHasFullyOpenedLid == timesPlayerOpensLidBeforeSummoning 
            && !demonSummoned)
        {
            demonSummoned = true;
            demonAnimator.SetTrigger(summonAnimTrigger);
            DemonSummoned?.Invoke();
        }
    }
    private void OnEnable()
    {
        ToiletLidOpenTrigger.ToiletLidFullyOpened += OnToiletLidFullyOpened;
        ToiletLidClosedTrigger.ToiletLidStartedOpening += OnToiletLidStartedOpening;
    }
    private void OnDisable()
    {
        ToiletLidOpenTrigger.ToiletLidFullyOpened -= OnToiletLidFullyOpened;
        ToiletLidClosedTrigger.ToiletLidStartedOpening -= OnToiletLidStartedOpening;
    }
}
