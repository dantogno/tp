using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    [SerializeField]
    private Transform objectToFollow;

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
            Debug.Log("Pee input pressed");
            if (!particleSystem.isPlaying)
                particleSystem.Play();
        }
        if (Input.GetButtonUp("Pee"))
        {
            particleSystem.Stop();
        }
    }
}
