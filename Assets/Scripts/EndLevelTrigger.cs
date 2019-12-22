using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zinnia.Visual;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField]
    private bool debugTestEndingSequence;

    [SerializeField]
    private float delayBeforeLoadingCredits = 3;

    public static event Action LevelEnded;
    private CameraColorOverlay cameraColorOverlay;
    private AudioSource audioSource;
    private bool isGateUnlocked;
    private bool isTriggered;

    private void Awake()
    {
        cameraColorOverlay = GetComponent<CameraColorOverlay>();
        audioSource = GetComponent<AudioSource>();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (debugTestEndingSequence)
            if (!isTriggered)
                DoEndLevelSequence();
    }
#endif
    private IEnumerator EndingSequenceCoroutine()
    {
        LevelEnded?.Invoke();
        yield return new WaitForSeconds(delayBeforeLoadingCredits);
        SceneManager.LoadScene("afterActionReview");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isGateUnlocked && !isTriggered)
        {
            DoEndLevelSequence();
        }
    }

    private void DoEndLevelSequence()
    {
        isTriggered = true;
        cameraColorOverlay.AddColorOverlay();
        audioSource.Play();
        StartCoroutine(EndingSequenceCoroutine());
    }

    private void OnGateUnlocked()
    {
        isGateUnlocked = true;
    }
    private void OnEnable()
    {
       // SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        GateController.GateUnlocked += OnGateUnlocked;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        GateController.GateUnlocked -= OnGateUnlocked;
    }
}
