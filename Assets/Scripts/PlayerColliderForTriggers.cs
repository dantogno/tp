using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderForTriggers : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - objectToFollow.position;
    }
    private void Update()
    {
        transform.position = objectToFollow.position + offset;
    }
}
