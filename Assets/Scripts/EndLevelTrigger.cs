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

    private CameraColorOverlay cameraColorOverlay;
    private AudioSource audioSource;
    private bool isGateUnlocked;
    private bool isTriggered;

    private void Awake()
    {
        cameraColorOverlay = GetComponent<CameraColorOverlay>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (debugTestEndingSequence)
            DoEndLevelSequence();
    }
    private IEnumerator EndingSequenceCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeLoadingCredits);
        SceneManager.LoadScene("creditsScene");
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
