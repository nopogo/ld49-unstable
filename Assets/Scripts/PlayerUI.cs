using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : Singleton<PlayerUI>{

    public TMP_Text scoreText;
    public TMP_Text livesText;

    public GameObject gameOverUI;
    public GameObject damagePopup;


    public float damagePopupTime = 0.2f;


    IEnumerator Start(){
        gameOverUI.SetActive(false);
        yield return new WaitUntil(()=> GameState.instance != null);
        GameState.instance.UpdateUIEvent += UpdateUI;
        GameState.instance.GameOverEvent += ShowGameOverCanvas;
        GameState.instance.DamageEvent   += OnPlayerDamage;
        UpdateUI();
    }

    void ShowGameOverCanvas(){
        gameOverUI.SetActive(true);
    }

    void OnPlayerDamage(float amount){
        StartCoroutine(ShowDamagePopup());
    }

    IEnumerator ShowDamagePopup(){
        damagePopup.SetActive(true);
        yield return new WaitForSeconds(damagePopupTime);
        damagePopup.SetActive(false);
    }


    

    void UpdateUI(){
        scoreText.text = $"Score: {GameState.instance.score}";
        string livesString = "";
        for (int i = 0; i < GameState.instance.playerLives; i++)
        {
            livesString += "I";
        }
        livesText.text = $"Lives: {livesString}";
    }
}
