using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private Transform objectToFollow;

    [SerializeField]
    private float rotationThreshold;

    [SerializeField]
    private float rotationYSpeed = 150.0f;

    [SerializeField]
    private float rotationXSpeed = 5.0f;

    [Range(0,90)]
    [Tooltip("0 to 1. 1 = 90 degrees.")]
    [SerializeField]
    private float rotationXMax = 0.5f;

    [Tooltip("Increase to make the pee stronger.")]
    [SerializeField]
    private float peeInputAxisMultiplier = 5.0f;

    [SerializeField]
    private AudioSource peeAudio;

    public static event Action PlayerStartedPeeing;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + offset;
        var peeInput = Input.GetAxis("PeeAxis");
        var peeInputAbs = Mathf.Abs(peeInput);
        
        if (peeInputAbs > 0)
        {
            PlayerStartedPeeing?.Invoke();
            if (!particleSystem.isPlaying)
            {
                SetInitialPeeingRotation();
                particleSystem.Play();
            }
            else
            {
                UpdateYRotationWhilePeeing();
                UpdateXRotationWhilePeeing(peeInput);
                var main = particleSystem.main;
                main.startSpeedMultiplier = peeInputAbs *= peeInputAxisMultiplier;
            }
            if (!peeAudio.isPlaying)
                peeAudio.Play();
        }
        else
        {
            particleSystem.Stop();
            peeAudio.Stop();
        }                        
    }

    /// <summary>
    /// If the player pulls back on the joystick, aim the pee upward.
    /// </summary>
    /// <param name="input"></param>
    private void UpdateXRotationWhilePeeing(float input)
    {
        if (input > 0)
        {
            var rotationXAdjustment = (rotationXSpeed * input) + transform.rotation.eulerAngles.x;
            // Debug.Log($"rotaionX: {transform.rotation.eulerAngles.x}");
            rotationXAdjustment = Mathf.Clamp(Mathf.Abs(rotationXAdjustment), 0, rotationXMax) * -1;
            var newRotation = Quaternion.Euler(rotationXAdjustment, transform.rotation.eulerAngles.y, 0);
            transform.rotation = newRotation;
        }        
    }


    /// <summary>
    /// When the player first starts peeing, snap the pee rotation Y aim to where the player is looking.
    /// </summary>
    private void SetInitialPeeingRotation()
    {
        var newRotation = Quaternion.LookRotation(objectToFollow.localEulerAngles);
        newRotation.eulerAngles = new Vector3(0, objectToFollow.localEulerAngles.y, 0);
        transform.rotation = newRotation;
    }

    /// <summary>
    /// If the difference between the player's Y rotation and the paricle
    /// system's Y rotation is greater than the threshold, then the
    /// particle system should begin to rotate along with the player.
    /// </summary>
    private void UpdateYRotationWhilePeeing()
    {
        if (Mathf.Abs(transform.eulerAngles.y - objectToFollow.eulerAngles.y) > rotationThreshold)
        {
            float step = rotationYSpeed * Time.deltaTime;
            var newRotation = Quaternion.RotateTowards(transform.rotation, objectToFollow.localRotation, step);
            newRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, newRotation.eulerAngles.y, 0);
            transform.rotation = newRotation;
        }
    }
}
