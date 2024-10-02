using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    float bulletSpeed = 10f;
    Rigidbody2D ownRigidBody;

    PlayerMovement playerMovement;
    float bulletXSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ownRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        bulletXSpeed = playerMovement.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ownRigidBody.velocity = new Vector2(bulletXSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
