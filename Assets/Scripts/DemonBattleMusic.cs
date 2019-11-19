using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBattleMusic : MonoBehaviour
{
    [Tooltip("We delay the battle music by the lenght of the jump scare stinger," +
        "minus this reduction to fine tune timing")]
    [SerializeField]
    private float delayReductionOnBattleLoopStart = 0.7f;

    [SerializeField]
    private AudioClip defeatStinger, battleLoop, jumpsScareStinger;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDemonSummoned()
    {
        // The jump scare stinger plays from another audio source,
        // but we want to wait for it before we play the battle music.
        audioSource.clip = battleLoop;
        audioSource.loop = true;
        audioSource.PlayDelayed(jumpsScareStinger.length - delayReductionOnBattleLoopStart);
    }

    private void OnDemonFlushed()
    {
        audioSource.Stop();
        audioSource.clip = defeatStinger;
        audioSource.loop = false;
        audioSource.Play();
    }
    private void OnEnable()
    {
        FlushDemon.DemonFlushed += OnDemonFlushed;
        SummonDemon.DemonSummoned += OnDemonSummoned;
    }

    private void OnDisable()
    {
        FlushDemon.DemonFlushed -= OnDemonFlushed;
        SummonDemon.DemonSummoned -= OnDemonSummoned;
    }
}
