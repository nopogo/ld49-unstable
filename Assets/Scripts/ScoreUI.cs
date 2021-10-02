using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : Singleton<ScoreUI>{

    TMP_Text scoreText;

    int score = 0;


    public override void  Awake(){
        base.Awake();
        scoreText = GetComponent<TMP_Text>();
        UpdateUI();
    }

    public void AddToScore(int amount){
        score += amount;
        UpdateUI();
    }

    void UpdateUI(){
        scoreText.text = $"Score: {score}";
    }
}
