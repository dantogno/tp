using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
