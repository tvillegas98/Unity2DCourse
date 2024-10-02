using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D ownRigidBody;
    CapsuleCollider2D ownCapsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        ownRigidBody = GetComponent<Rigidbody2D>();
        ownCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ownRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyDirection();
    }

    void FlipEnemyDirection()
    {
        transform.localScale = new Vector2(-Mathf.Sign(ownRigidBody.velocity.x), 1f);
    }
}
