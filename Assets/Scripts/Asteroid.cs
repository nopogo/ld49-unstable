using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour {
    public Rigidbody asteroidRigidbody;

    public int points = 50;

    void Awake(){
        asteroidRigidbody = GetComponent<Rigidbody>();
    }




    // public void ApplyForceAtPoint(Vector3 origin, RaycastHit hit, float forceStrength, ForceType forceType){
    //     var forceDir = (origin - hit.point).normalized;
    //     forceDir *= forceStrength;
    //     if(forceType == ForceType.Push){
    //         forceDir *= -1;
    //     }
    //     asteroidRigidbody.AddForceAtPosition(forceDir, hit.point, ForceMode.Force);
    // }
}
