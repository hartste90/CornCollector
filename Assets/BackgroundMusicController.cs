using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {

    public float volumeIncrease = .1f;
    public float volumeDecrease = .2f;

    private AudioSource audioSource;
    private bool volumeIncreasing;
    private bool volumeDecreasing;


	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
		
	}

    private void Update()
    {
        if(volumeIncreasing)
        {
            audioSource.volume += volumeIncrease;
            if (audioSource.volume >= 1)
            {
                volumeIncreasing = false;
            }
        }
        else if (volumeDecreasing)
        {
            audioSource.volume -= volumeDecrease;
            if (audioSource.volume <= 0)
            {
                volumeDecreasing = false;
                audioSource.Stop();
            }
        }
    }

    public void fadeInBackgroundMusic()
    {
        audioSource.volume = 0;
        volumeIncreasing = true;
        audioSource.Play();
    }

    public void fadeOutBackgroundMusic()
    {
        volumeDecreasing = true;
    }
}
