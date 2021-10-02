using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    float horizontalAxis = 0f;
    float verticalAxis = 0f;
    bool killMomentum = false;

    //Constants
    public float jetPackForceMultiplier = 1f;
    public float dragMultiplier = 5f;
    public float torque = 1f;

    float angularDragStart;
    float dragStart;
    float rollDirection = 0f;    

    Rigidbody playerRigidbody;


    void Awake(){
        playerRigidbody     = GetComponent<Rigidbody>();
        angularDragStart    = playerRigidbody.angularDrag;
        dragStart           = playerRigidbody.drag;
    }


    void FixedUpdate(){
        PlayerMovementInput();
        ApplyPlayerMovementForce();
    }

    void PlayerMovementInput(){
        if(Input.GetKey(KeyCode.LeftControl)){
            killMomentum     = true;
            return;
        }else{
            killMomentum = false;
        }

        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        rollDirection = 0f;

        if(Input.GetKey(KeyCode.Q)){
            rollDirection = 1f;
        }
        if(Input.GetKey(KeyCode.E)){
            rollDirection = -1f;
        }
    }


    void ApplyPlayerMovementForce(){
        //Kill momentum
        if(killMomentum){
            playerRigidbody.drag = dragMultiplier;
            playerRigidbody.angularDrag = dragMultiplier;
            return;
        }else{
            playerRigidbody.drag = dragStart;
            playerRigidbody.angularDrag = angularDragStart;
        }

        //Up down
        playerRigidbody.AddForce(transform.up * verticalAxis * jetPackForceMultiplier, ForceMode.Impulse);
        //Move left right
        playerRigidbody.AddForce(transform.right * horizontalAxis * jetPackForceMultiplier, ForceMode.Impulse);
        //Roll
        playerRigidbody.AddTorque(transform.forward * rollDirection * torque, ForceMode.Impulse);
    }
    
}
