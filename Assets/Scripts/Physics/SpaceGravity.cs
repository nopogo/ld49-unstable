using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class SpaceGravity : MonoBehaviour{

    float randomForceMax = 0.15f;
    float randomForceMin = 0.1f;

    void Awake(){
        RandomInitialTorque();
    }


    void RandomInitialTorque(){
        Vector3 initialForce = new Vector3(Random.Range(randomForceMin, randomForceMax), Random.Range(randomForceMin, randomForceMax), Random.Range(randomForceMin, randomForceMax));
        GetComponent<Rigidbody>().AddTorque(initialForce, ForceMode.Impulse);
    }
}