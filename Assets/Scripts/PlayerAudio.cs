using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : Singleton<PlayerAudio> {
    
    public AudioClip[] damageSounds;
    public AudioClip[] hurtSounds;

    public AudioClip deathSound;

    public AudioClip points;

    AudioSource audioSource;

    public AudioSource heartbeatSource;

    public AudioSource laser1Source;
    public AudioSource laser2Source;


    public AudioClip[] musicLayer1;
    public AudioClip[] musicLayer2;
    public AudioClip[] musicLayer3;

    public AudioSource musicPlayer1;
    public AudioSource musicPlayer2;
    public AudioSource musicPlayer3;

    IEnumerator Start(){
        audioSource = GetComponent<AudioSource>();
        yield return new WaitUntil(()=> GameState.instance != null);
        GameState.instance.DamageEvent += PlayRandomDamageSound;
        GameState.instance.scoreEvent  += PlayScoreEventSound;
        // GameState.instance. += PlayRandomDamageSound;
        StartCoroutine(MusicTrack(musicLayer1, musicPlayer1));
        StartCoroutine(MusicTrack(musicLayer2, musicPlayer1));
        StartCoroutine(MusicTrack(musicLayer3, musicPlayer1));
    }

    IEnumerator MusicTrack(AudioClip[] possibleClips, AudioSource musicTrackAudioSource){
        while(GameState.instance.isGameOver == false){
            yield return new WaitForSeconds(Random.Range(1f, 15f));
            AudioClip newClip = possibleClips[Random.Range(0, possibleClips.Length)];
            yield return new WaitUntil(()=> audioSource.isPlaying == false);
            musicTrackAudioSource.PlayOneShot(newClip);
            yield return new WaitForSeconds(newClip.length);
        }
    }

    void PlayRandomDamageSound(float amount){
        if(amount <= GameState.instance.maxDamageAllowed){
            return;
        }
        StartCoroutine(PlayDamageSequence());

        if(GameState.instance.playerLives < 2){
            heartbeatSource.mute = false;
        }else{
            heartbeatSource.mute = true;
        }
    }

    void PlayScoreEventSound(){
        audioSource.PlayOneShot(points);
    }

    IEnumerator PlayDamageSequence(){
        audioSource.PlayOneShot(damageSounds[Random.Range(0, damageSounds.Length)]);
        yield return new WaitForSeconds(.4f);
        if(GameState.instance.playerLives > 0){
            audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length)]);
        }else{
            heartbeatSource.mute = true;
            audioSource.PlayOneShot(deathSound);
        }
        
    }


    // public AudioClip GetDamageSound(){

    // }

    // public AudioClip GetHurtSound(){

    // }
}
