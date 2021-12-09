using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource inGameSound;
    public AudioSource thanksSound;
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
}
