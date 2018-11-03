using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour {


    public AudioClip playerDeathSoundClip;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlayPlayerDeathSound()
    {
        audioSource.clip = playerDeathSoundClip;
        audioSource.Play();
    }
}
