using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;

    [SerializeField]
    int coinValue = 100;
    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {

            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
            FindObjectOfType<GameSession>().IncreaseCoinScore(coinValue);
            gameObject.SetActive(false);
            wasCollected = true;
        }
    }
}
