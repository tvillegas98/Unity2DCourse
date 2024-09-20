using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueForceAmount = 1f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float maxSpeed = 40f;
    Rigidbody2D rBody2D;
    SurfaceEffector2D surfaceEffector2D;

    bool canMove = true;

    float timePressed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove){
            RotatePlayer();
            BoostPlayerSpeed();
        }
    }

    void RotatePlayer(){
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
            rBody2D.AddTorque(torqueForceAmount);
        }else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
            rBody2D.AddTorque(-torqueForceAmount);
        }
    }

    void BoostPlayerSpeed(){
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
            timePressed += Time.deltaTime;
            float newSpeed = CalculateAcceleration(timePressed, baseSpeed, this.acceleration);
            IncreaseSurfaceSpeed(newSpeed);
        }else{
            surfaceEffector2D.speed = baseSpeed;
            timePressed = 0;
        }
    }
    void IncreaseSurfaceSpeed(float speed){
        if (speed >= maxSpeed){
            surfaceEffector2D.speed = maxSpeed;
        }else{
            surfaceEffector2D.speed = speed;
        }
    }

    float CalculateAcceleration(float time, float baseSpeed, float acceleration){
        return baseSpeed + acceleration * time;
    }

    public void DisableControls(){
        canMove = false;
    }
}
