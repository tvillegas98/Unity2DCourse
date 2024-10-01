using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float playerHorizontalSpeed = 3f;

    [SerializeField]
    float playerClimbingSpeed = 3f;

    [SerializeField]
    float singleJumpSpeed = 1f;

    [SerializeField]
    float defaultGravityScale = 8f;
    Animator ownAnimator;

    Vector2 moveInput;
    Rigidbody2D ownRigidBody;

    Collider2D ownCapsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        ownRigidBody = GetComponent<Rigidbody2D>();
        ownAnimator = GetComponent<Animator>();
        ownCapsuleCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!ownCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        ownRigidBody.velocity += new Vector2(0f, singleJumpSpeed);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(
            moveInput.x * playerHorizontalSpeed,
            ownRigidBody.velocity.y
        );
        ownRigidBody.velocity = playerVelocity;
        ownAnimator.SetBool("isRunning", true);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(ownRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(ownRigidBody.velocity.x), 1f);
        }
        else
        {
            ownAnimator.SetBool("isRunning", false);
        }
    }

    void ClimbLadder()
    {
        if (!ownCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            ownRigidBody.gravityScale = defaultGravityScale;
            ownAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbingVelocity = new Vector2(
            ownRigidBody.velocity.x,
            moveInput.y * playerClimbingSpeed
        );

        ownRigidBody.velocity = climbingVelocity;
        ownRigidBody.gravityScale = 0;
        ownAnimator.SetBool("isClimbing", true);

        bool playerHasClimbingSpeed = Mathf.Abs(ownRigidBody.velocity.y) > Mathf.Epsilon;
        ownAnimator.SetBool("isClimbing", playerHasClimbingSpeed);

    }
}
