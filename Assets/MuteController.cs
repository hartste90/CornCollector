using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteController : MonoBehaviour {

    public BackgroundMusicController bgController;
    public SoundEffectsController sfController;
    public string muteText;
    public Image muteImage;
    public Animator[] muteTextAnimatorList;

    public Sprite mutedSprite;
    public Sprite unmutedSprite;


    private bool isMuted;


	// Use this for initialization
	void Awake () {
        isMuted = false;

	}

    public void SetMuted(bool shouldBeMuted)
    {
        isMuted = shouldBeMuted;
        bgController.gameObject.GetComponent<AudioSource>().mute = shouldBeMuted;
        sfController.gameObject.GetComponent<AudioSource>().mute = shouldBeMuted;
        if (shouldBeMuted)
        {
            muteText = "muted";
            muteImage.sprite = mutedSprite;
        }
        else
        {
            muteText = "unmuted";
            muteImage.sprite = unmutedSprite;
        }
        foreach(Animator anim in muteTextAnimatorList)
        {
            anim.GetComponent<Text>().text = muteText;
            anim.ResetTrigger("Show");
            anim.SetTrigger("Show");
        }

    }

    public void ToggleMute()
    {
        SetMuted(!isMuted);
    }
}
