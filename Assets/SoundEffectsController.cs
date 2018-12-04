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
        audioSource.clip = playerDeathSoundClip;
        audioSource.Play();
    }
    public void PlayPlayerTurnSound()
    {
        audioSource.clip = playerTurnSoundClip;
        audioSource.Play();
    }
    public void PlayMineExplodeSound()
    {
        audioSource.clip = mineExplodeSoundClip;
        audioSource.Play();
    }
    public void PlaySafeBustSound()
    {
        audioSource.clip = safeBustSoundClip;
        audioSource.Play();
    }
    public void PlayCoinCollectedSound()
    {
        audioSource.clip = playerDeathSoundClip;
        audioSource.Play();
    }
    public void PlayUIMinorSound()
    {
        audioSource.clip = uiMinorSoundClip;
        audioSource.Play();
    }
    public void PlayUIMajorSound()
    {
        audioSource.clip = uiMajorSoundClip;
        audioSource.Play();
    }
}
