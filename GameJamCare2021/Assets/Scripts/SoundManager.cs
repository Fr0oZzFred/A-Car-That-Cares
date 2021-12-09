using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource inGameSound;
    public AudioSource thanksSound;
    public AudioSource carSound;
    public AudioSource victorySound;
    public AudioSource buttonSound;
    private void Awake() {
        Instance = this;
    }

    public void PlayInGameTheme() {
        inGameSound.Play();
    }

    public void PlayThanksEffect() {
        if(!thanksSound.isPlaying)
        thanksSound.Play();
    } 

    public void PlayCarEffect() {
        if(!carSound.isPlaying)
        carSound.Play();
    }
    
    public void PlayVictoryEffect() {
        if(!victorySound.isPlaying)
        victorySound.Play();
    }  
    
    public void PlaybuttonEffect() {
        buttonSound.Play();
    }
}
