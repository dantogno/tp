using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPeeInToilet : MonoBehaviour
{
    
    // public static event Action PeeDetectedInToilet;
    [SerializeField]
    private int numberOfParticlesBeforeClosingLid = 30;

    [SerializeField]
    private int timesToClose = 1;

    [SerializeField]
    private AudioSource toiletBowlAudio;

    private int numberOfParticlesSoFar, timesClosed = 0;
    private ToiletLidPusher toiletLidPusher;
    private bool waitForFullyOpen = false;

    private void Awake()
    {
        toiletLidPusher = FindObjectOfType<ToiletLidPusher>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (!waitForFullyOpen)
        {
            numberOfParticlesSoFar++;
        }

        if (!toiletBowlAudio.isPlaying)
        {
            // toiletBowlAudio.Play();
            // TODO figure out logic for this!
        }

        if (numberOfParticlesSoFar > numberOfParticlesBeforeClosingLid
            && timesClosed < timesToClose)
        {
            toiletLidPusher.PushLidClosed();
            timesClosed++;
            waitForFullyOpen = true;
            numberOfParticlesSoFar = 0;
        }
    }


    private void OnToiletLidFullyOpened()
    {
        waitForFullyOpen = false;
    }
    private void OnEnable()
    {
        ToiletLidOpenTrigger.ToiletLidFullyOpened += OnToiletLidFullyOpened;   
    }
    private void OnDisable()
    {
        ToiletLidOpenTrigger.ToiletLidFullyOpened -= OnToiletLidFullyOpened;
    }
}
