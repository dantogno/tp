using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private Transform playerHeadTransform;

    private void Start()
    {
        // Adjust Y to match player height
        GameObject playerHeadGameObject = GameObject.Find("HeadsetAlias");
        Vector3 newPostion = transform.position;
        newPostion.y = playerHeadGameObject.transform.position.y;
        transform.position = newPostion;
    }
}
