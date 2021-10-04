using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AudioSetting {Unmuted, Muted}

public class GameState : Singleton<GameState> {

    public bool isGameOver = false;
    public bool hasStarted = false;

    public Action GameOverEvent;
    public Action UpdateUIEvent;
    public Action <float> DamageEvent;
    public Action scoreEvent;

    public AudioSetting audioSetting = AudioSetting.Unmuted;
    public BoxCollider shipCollider;


    public TMP_Text muteUnmuteText;
    public Animator shipAnimator;
    public LineRenderer playerTether;


    public int score = 0;

    public int playerLives = 3;
    public float maxDamageAllowed = 1.5f;

    AudioListener audioListener;

    void Start(){
        DamageEvent   += OnPlayerReceivedDamage;
        GameOverEvent += OnGameOver;
        audioListener = FindObjectOfType<AudioListener>();
    }



    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            audioSetting = audioSetting == AudioSetting.Unmuted ? AudioSetting.Muted : AudioSetting.Unmuted;
            audioListener.enabled = audioSetting == AudioSetting.Unmuted;
            if(audioListener.enabled){
                muteUnmuteText.text = "Mute";
            }else{
                muteUnmuteText.text = "Unmute";
            }
        }
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
        scoreEvent?.Invoke();
        UpdateUIEvent?.Invoke();
    }

    public void ResetLvl(){
        SceneManager.LoadScene(0);
    }

    public void StartGame(){
        StartCoroutine(StartGameSequence());
    }

    IEnumerator StartGameSequence(){
        shipAnimator.SetTrigger("Start");
        hasStarted = true;
        PlayerMovement.instance.verticalAxis = 1f;
        yield return new WaitUntil(()=> PlayerMovement.instance.transform.position.y >=.2f);
        playerTether.enabled = true;
        yield return new WaitForSeconds(5f);
        
        
        yield return new WaitUntil(()=> PlayerMovement.instance.transform.position.y >=5);
        PlayerMovement.instance.verticalAxis = 0f;
        shipCollider.enabled = true;        
    }



    
}
