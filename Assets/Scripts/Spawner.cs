using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner> {
    
    
    // PlayerMovement playerMovement;

    List<Asteroid> spawnedAsteroids = new List<Asteroid>();

    public GameObject asteroidPrefab;
    public BoxCollider spawnZone;

    public int maxAsteroids;

    

    public float minAsteroidWait = 1f;
    public float maxAsteroidWait = 10f;

    public float minAsteroidScale = .5f;
    public float maxAsteroidScale = 5;




    IEnumerator Start(){
        // Debug.Log($"1");
        yield return new WaitUntil(()=> DestroyAsteroids.instance != null);
        DestroyAsteroids.instance.AsteroidDestroyed += OnAsteroidDestroyed;
        // Debug.Log($"2");
        // yield return new WaitUntil(()=> PlayerMovement.instance != null);
        // playerMovement = PlayerMovement.instance;
        // Debug.Log($"3");
        for (int i = 0; i < 15; i++)
        {
            SpawnAsteroid();
        }
        StartCoroutine(SpawnAsteroids());
    }


    void OnDisable(){
        DestroyAsteroids.instance.AsteroidDestroyed -= OnAsteroidDestroyed;
    }

    IEnumerator SpawnAsteroids(){
        Debug.Log($"startSpawnin");
        while(GameState.instance.isGameOver == false){
            yield return new WaitUntil(()=> spawnedAsteroids.Count < maxAsteroids);
            float waitTime = Random.Range(minAsteroidWait, maxAsteroidWait);
            yield return new WaitForSeconds(waitTime);
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid(){
        Asteroid tempAsteroid = Instantiate(asteroidPrefab, RandomSpawnLocation(), Random.rotation).GetComponent<Asteroid>();
            float asteroidScale = Random.Range(minAsteroidScale, maxAsteroidScale);
            tempAsteroid.Initialize(asteroidScale);
            spawnedAsteroids.Add(tempAsteroid);
    }



    Vector3 RandomSpawnLocation(){
        Vector3 tempVector3 = Vector3.zero;
        tempVector3.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        tempVector3.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
        return tempVector3;
    }



    void OnAsteroidDestroyed(Asteroid asteroid){
        spawnedAsteroids.Remove(asteroid);
    }


    
}
