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

    [SerializeField]
    private Transform demonPhysicsTransform;

    private int summonAnimTrigger = Animator.StringToHash("Summon");
    private Vector3 originalPosition;
    private void OnEnable()
    {
        originalPosition = demonPhysicsTransform.position;
        demonAnimator.SetTrigger(summonAnimTrigger);
        jumpScareAudio.PlayDelayed(jumpScareAudioDelay);
    }
    private void OnDisable()
    {
        demonAnimator.SetTrigger(summonAnimTrigger);
        demonPhysicsTransform.position = originalPosition;
    }
}
