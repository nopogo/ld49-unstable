using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroids : Singleton<DestroyAsteroids> {
    
    public Action <Asteroid> AsteroidDestroyed;

    void OnTriggerEnter(Collider other) {
        Asteroid asteroid = other.GetComponent<Asteroid>();
        if( asteroid != null){
            Destroy(other.gameObject);
            GameState.instance.AddToScore(asteroid.points);
            AsteroidDestroyed?.Invoke(asteroid);
        }
        
    }
}
