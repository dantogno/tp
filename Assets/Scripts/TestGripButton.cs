using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGripButton : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToDisable;
    public void TestButton()
    {
        objectToDisable.SetActive(false);
    }
}
