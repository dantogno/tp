using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareDimensionDemon : MonoBehaviour
{
    [SerializeField]
    private AudioSource jumpScareAudio;

    [SerializeField]
    private Animator demonAnimator;

    [SerializeField]
    private float jumpScareAudioDelay = 0.15f;

    private int summonAnimTrigger = Animator.StringToHash("Summon");

    private void OnEnable()
    {
        demonAnimator.SetTrigger(summonAnimTrigger);
        jumpScareAudio.PlayDelayed(jumpScareAudioDelay);
    }
}
