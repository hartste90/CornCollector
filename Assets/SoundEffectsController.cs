using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour {


    public AudioClip playerDeathSoundClip;
    public AudioClip playerTurnSoundClip;
    public AudioClip mineExplodeSoundClip;
    public AudioClip safeBustSoundClip;
    public AudioClip coinCollectedSoundClip;
    public AudioClip uiMinorSoundClip;
    public AudioClip uiMajorSoundClip;

    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlayPlayerDeathSound()
    {
        audioSource.volume = .1f;
        audioSource.clip = playerDeathSoundClip;
        audioSource.Play();
    }
    public void PlayPlayerTurnSound()
    {
        audioSource.volume = .1f;
        audioSource.clip = playerTurnSoundClip;
        audioSource.Play();
    }
    public void PlayMineExplodeSound()
    {
        audioSource.volume = .1f;
        audioSource.clip = mineExplodeSoundClip;
        audioSource.Play();
    }
    public void PlaySafeBustSound()
    {
        audioSource.volume = .1f;
        audioSource.clip = safeBustSoundClip;
        audioSource.Play();
    }
    public void PlayCoinCollectedSound()
    {
        audioSource.volume = .1f;
        audioSource.clip = playerDeathSoundClip;
        audioSource.Play();
    }
    public void PlayUIMinorSound()
    {
        audioSource.volume = .03f;
        audioSource.clip = uiMinorSoundClip;
        audioSource.Play();
    }
    public void PlayUIMajorSound()
    {
        audioSource.volume = .1f;

        audioSource.clip = uiMajorSoundClip;
        audioSource.Play();
    }
}
