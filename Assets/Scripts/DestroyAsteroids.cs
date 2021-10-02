using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroids : MonoBehaviour {
    
    Action AsteroidDestroyed;

    void OnTriggerEnter(Collider other) {
        Asteroid asteroid = other.GetComponent<Asteroid>();
        if( asteroid != null){
            Destroy(other.gameObject);
            ScoreUI.instance.AddToScore(asteroid.points);
            AsteroidDestroyed?.Invoke();
        }
        
    }
}
