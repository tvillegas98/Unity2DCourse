using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    float reloadSceneDelay = 0.5f;

    [SerializeField]
    ParticleSystem finishEffect;

    bool hasFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!hasFinished)
            {
                hasFinished = true;
                finishEffect.Play();
                GetComponent<AudioSource>().Play();
                Invoke("ReloadScene", reloadSceneDelay);
            }
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
