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

    [Tooltip("If the player hasn't peed in the toilet for this long, stop the sound effect.")]
    [SerializeField]
    private float stoppedPeeingInBowlCooldown = 0.1f;

    private int numberOfParticlesSoFar, timesClosed = 0;
    private ToiletLidPusher toiletLidPusher;
    private bool waitForFullyOpen = false;
    private float lastPeedInToiletTime;

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
            toiletBowlAudio.Play();
        }

        if (numberOfParticlesSoFar > numberOfParticlesBeforeClosingLid
            && timesClosed < timesToClose)
        {
            toiletLidPusher.PushLidClosed();
            timesClosed++;
            waitForFullyOpen = true;
            numberOfParticlesSoFar = 0;
        }
        lastPeedInToiletTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - lastPeedInToiletTime > stoppedPeeingInBowlCooldown
            && toiletBowlAudio.isPlaying)
            toiletBowlAudio.Stop();

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
