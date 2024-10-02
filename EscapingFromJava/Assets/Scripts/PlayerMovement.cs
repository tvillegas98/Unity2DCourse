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

    Collider2D ownBodyCollider;
    BoxCollider2D ownFeetCollider;

    float deathBounceX = 2f;
    float deathBounceY = 10f;

    bool isAlive = true;


    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform spawnBulletPoint;
    // Start is called before the first frame update
    void Start()
    {
        ownRigidBody = GetComponent<Rigidbody2D>();
        ownAnimator = GetComponent<Animator>();
        ownBodyCollider = GetComponent<Collider2D>();
        ownFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        if (isAlive)
        {

            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {
        if (isAlive)
        {
            Jump();
        }
    }

    private void Jump()
    {

        if (!ownFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        ownRigidBody.gravityScale = defaultGravityScale;
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
        if (!ownFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
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
        bool playerHasClimbingSpeed = Mathf.Abs(ownRigidBody.velocity.y) > Mathf.Epsilon;
        ownAnimator.SetBool("isClimbing", playerHasClimbingSpeed);

    }

    void OnFire(InputValue value){
        if (isAlive)
        {
            ownAnimator.SetTrigger("Shooting");
            Instantiate(bullet, spawnBulletPoint.position, spawnBulletPoint.rotation);
        }
    }

    void Die(){
        if(isAlive && ownBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))){
            isAlive = false;
            ownAnimator.SetTrigger("Dying");
            ownRigidBody.velocity = new Vector2(-ownRigidBody.velocity.x * deathBounceX, deathBounceY);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        };
    }
}
