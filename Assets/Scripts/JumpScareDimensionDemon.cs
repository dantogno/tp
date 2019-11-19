using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareDimensionDemon : MonoBehaviour
{
    [SerializeField]
    private AudioSource jumpScareAudio;

 
    private void OnEnable()
    {
        jumpScareAudio.Play();
    }
}
