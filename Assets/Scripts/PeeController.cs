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
    private float rotationSpeed = 1.0f;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position + offset;

        if (Input.GetButton("Pee"))
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
                SetInitialPeeingRotation();
            }
            else
                UpdateRotationWhilePeeing();       
        }
        if (Input.GetButtonUp("Pee"))
        {
            particleSystem.Stop();
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
    private void UpdateRotationWhilePeeing()
    {
        if (Mathf.Abs(transform.eulerAngles.y - objectToFollow.eulerAngles.y) > rotationThreshold)
        {
            float step = rotationSpeed * Time.deltaTime;
            var newRotation = Quaternion.RotateTowards(transform.rotation, objectToFollow.localRotation, step);
            newRotation.eulerAngles = new Vector3(0, newRotation.eulerAngles.y, 0);
            transform.rotation = newRotation;
        }
    }
}
