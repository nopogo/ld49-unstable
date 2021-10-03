using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : Singleton<GameState> {

    public bool isGameOver = false;

    public Action GameOverEvent;
    public Action UpdateUIEvent;
    public Action <float> DamageEvent;


    public int score = 0;

    public int playerLives = 3;
    public float maxDamageAllowed = 1.5f;

    void Start(){
        DamageEvent   += OnPlayerReceivedDamage;
        GameOverEvent += OnGameOver;
    }

    void OnPlayerReceivedDamage(float amount){
        if(amount <= maxDamageAllowed){
            return;
        }
        playerLives -= 1;
        if(playerLives <= 0){
            GameOverEvent?.Invoke();
        }
        UpdateUIEvent?.Invoke();
    }
    
    void OnGameOver(){
        isGameOver = true;
    }

    public void AddToScore(int amount){
        score += amount;
        UpdateUIEvent?.Invoke();
    }

    public void ResetLvl(){
        SceneManager.LoadScene(0);
    }



    
}
