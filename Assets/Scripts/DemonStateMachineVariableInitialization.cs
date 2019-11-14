using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStateMachineVariableInitialization : MonoBehaviour
{
    private void OnEnable()
    {
        var animator = GetComponent<Animator>();
        var audioSource = GetComponent<AudioSource>();
        var audioClipSMBs = animator.GetBehaviours<PlayAudioClipSMB>();
        foreach (var item in audioClipSMBs)
        {
            item.Audio = audioSource;
        }
    }
}
