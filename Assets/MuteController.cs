using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteController : MonoBehaviour {

    public BackgroundMusicController bgController;
    public SoundEffectsController sfController;
    public Text muteText;
    public Image muteImage;

    public Sprite mutedSprite;
    public Sprite unmutedSprite;

    private Animator muteTextAnimator;
    private bool isMuted;


	// Use this for initialization
	void Awake () {
        muteTextAnimator = muteText.gameObject.GetComponent<Animator>();
	}

    public void SetMuted(bool shouldBeMuted)
    {
        isMuted = shouldBeMuted;
        bgController.gameObject.GetComponent<AudioSource>().mute = shouldBeMuted;
        sfController.gameObject.GetComponent<AudioSource>().mute = shouldBeMuted;
        if (shouldBeMuted)
        {
            muteText.text = "muted";
            muteImage.sprite = mutedSprite;
        }
        else
        {
            muteText.text = "unmuted";
            muteImage.sprite = unmutedSprite;
        }
        muteTextAnimator.ResetTrigger("Show");
        muteTextAnimator.SetTrigger("Show");
    }

    public void ToggleMute()
    {
        SetMuted(!isMuted);
    }
}
