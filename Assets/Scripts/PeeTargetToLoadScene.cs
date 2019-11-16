using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zinnia.Visual;

public class PeeTargetToLoadScene : MonoBehaviour
{
    [SerializeField]
    private int numberOfParticlesToTriggerLoad = 45;

    [SerializeField]
    private List<GameObject> objectsToStopBeforeLoad;

    [SerializeField]
    private AudioSource stingerToPlayOnLoad;

    private int particlesSoFar = 0;

    private CameraColorOverlay cameraColorOverlay;
    private void Start()
    {
        cameraColorOverlay = GetComponent<CameraColorOverlay>();
    }
    private void OnParticleCollision(GameObject other)
    {
        particlesSoFar++;

        if (particlesSoFar > numberOfParticlesToTriggerLoad)
        {
            cameraColorOverlay.AddColorOverlay();
            foreach (var item in objectsToStopBeforeLoad)
            {
                item.SetActive(false);
            }
            stingerToPlayOnLoad.Play();
            StartCoroutine(LoadCoroutine());
        }
    }

    private IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(cameraColorOverlay.AddDuration);
        SceneManager.LoadScene("small_bathroom");
    }
}
