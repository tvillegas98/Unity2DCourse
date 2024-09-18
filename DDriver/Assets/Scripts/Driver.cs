using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steeringSpeed = 300f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 15f;
    [SerializeField] int health = 100;

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steeringSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, -moveAmount, 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (health > 0){
            health -= 10;
            moveSpeed = slowSpeed;
        }
        else{
            Debug.Log("You car is broken");
            moveSpeed = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "SpeedBoost":
                moveSpeed = boostSpeed;
                break;
            case "Bump":
                moveSpeed = slowSpeed;
                break;
        }
    }
}
