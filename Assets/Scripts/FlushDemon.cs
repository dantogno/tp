using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class FlushDemon : MonoBehaviour
{
    [SerializeField]
    private Animator demonAnimator;

    public static event Action DemonFlushed;
    private int flushAnim = Animator.StringToHash("Flush");
    private bool demonSummoned = false;
    private bool demonFlushed = false;
    private void OnToiletFlushed()
    {
        if (demonSummoned & !demonFlushed)
        {
            demonAnimator.SetTrigger(flushAnim);
            demonFlushed = true;
            DemonFlushed?.Invoke();
        }
    }
    private void OnDemonSummoned()
    {
        demonSummoned = true;
    }
    private void OnEnable()
    {
        FlushController.ToiletFlushed += OnToiletFlushed;
        SummonDemon.DemonSummoned += OnDemonSummoned;
    }
    private void OnDisable()
    {
        FlushController.ToiletFlushed -= OnToiletFlushed;
        SummonDemon.DemonSummoned -= OnDemonSummoned;
    }
}
