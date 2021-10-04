using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PlayerMovement : Singleton<PlayerMovement> {


    float horizontalAxis = 0f;
    public float verticalAxis = 0f;
    bool killMomentum = false;

    //Constants
    public float jetPackForceMultiplier = 1f;
    public float dragMultiplier = 5f;
    public float torque = 1f;

    float angularDragStart;
    float dragStart;
    float rollDirection = 0f;    

    Rigidbody playerRigidbody;

    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;
    public ParticleSystem upParticles;
    public ParticleSystem downParticles;

    public Light jetpackLight;

    public AudioSource jetPackSource;


    EmissionModule rightSideEmission;
    EmissionModule leftSideEmission;
    EmissionModule topSideEmission;
    EmissionModule downSideEmission;
    

    public override void Awake(){
        base.Awake();
        playerRigidbody     = GetComponent<Rigidbody>();
        angularDragStart    = playerRigidbody.angularDrag;
        dragStart           = playerRigidbody.drag;
        rightSideEmission   = rightParticles.emission;
        leftSideEmission    = leftParticles.emission;
        topSideEmission     = upParticles.emission;
        downSideEmission    = downParticles.emission;
    }


    void FixedUpdate(){
        if(GameState.instance.isGameOver || GameState.instance.hasStarted == false){
            jetPackSource.mute        = true;
            jetpackLight.enabled      = false;
            rightSideEmission.enabled = false;
            leftSideEmission.enabled  = false;
            downSideEmission.enabled  = false;
            topSideEmission.enabled   = false;
            return;
        }
        PlayerMovementInput();
        ApplyPlayerMovementForce();
        ApplyParticles();
        ApplyLightAndSound();
    }

    void PlayerMovementInput(){
        if(Input.GetKey(KeyCode.LeftShift)){
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



    void OnCollisionEnter(Collision coll){
        if(coll.transform.tag == "harmless"){
            return;
        }
        GameState.instance.DamageEvent?.Invoke(coll.relativeVelocity.magnitude);
    }

    void ApplyParticles(){
        rightSideEmission.enabled = killMomentum || horizontalAxis < 0 || rollDirection > 0;
        leftSideEmission.enabled  = killMomentum || horizontalAxis > 0 || rollDirection < 0;
        downSideEmission.enabled  = killMomentum || verticalAxis > 0;
        topSideEmission.enabled   = killMomentum || verticalAxis < 0;        
    }

    void ApplyLightAndSound(){
        if(rightSideEmission.enabled || leftSideEmission.enabled || downSideEmission.enabled || topSideEmission.enabled){
            jetpackLight.enabled = true;
            jetPackSource.mute   = false;
        }else{
            jetpackLight.enabled = false;
            jetPackSource.mute   = true;
        }
        if(rightSideEmission.enabled && leftSideEmission.enabled == false){
            jetPackSource.panStereo = .7f;
        }else if(rightSideEmission.enabled == false && leftSideEmission.enabled){
            jetPackSource.panStereo = -.7f;
        }else{
            jetPackSource.panStereo = 0f;
        }
    }
    
}
