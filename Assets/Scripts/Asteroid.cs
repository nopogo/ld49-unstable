using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour {
    public Rigidbody asteroidRigidbody;

    public int points     = 50;
    public float baseMass = 50;

    void Awake(){
        asteroidRigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(float asteroidScale){
        transform.localScale = new Vector3(asteroidScale, asteroidScale, asteroidScale);

        asteroidRigidbody.mass = baseMass * asteroidScale;
        points = Mathf.RoundToInt(baseMass * asteroidScale);
    }


}
