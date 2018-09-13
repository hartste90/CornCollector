using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {

    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
		
	}

    public void playBackgroundMusic()
    {
        audioSource.Play();
    }
}
