using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float  reloadSceneDelay = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    bool crashed = false;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground")
        {
            if(!crashed){
                crashed = true;
                FindObjectOfType<PlayerController>().DisableControls();
                GetComponent<AudioSource>().PlayOneShot(crashSFX);
                crashEffect.Play();
                Invoke("ReloadScene", reloadSceneDelay);
            }

        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(0);
    }
}
