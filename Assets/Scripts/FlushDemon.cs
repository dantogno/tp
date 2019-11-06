using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Prefabs.Interactions.Controllables;

public class FlushDemon : MonoBehaviour
{
    [SerializeField]
    private Animator demonAnimator;

    private int flushAnim = Animator.StringToHash("Flush");
    private void OnToiletFlushed()
    {
        demonAnimator.SetTrigger(flushAnim);
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
