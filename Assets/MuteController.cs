using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
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
	void Start () {
        isMuted = PlayerPrefManager.GetMuteState();
        if (isMuted)
        {
            SetMuted(true);
        }

	}

    public void SetMuted(bool shouldBeMuted)
    {

        Analytics.CustomEvent("mute", new Dictionary<string, object>
        {
            { "isMuted", shouldBeMuted }
        });

        isMuted = shouldBeMuted;
        bgController.gameObject.GetComponent<AudioSource>().mute = shouldBeMuted;
        sfController.gameObject.GetComponent<AudioSource>().mute = shouldBeMuted;
        if (shouldBeMuted)
        {
            muteText = "muted";
            muteImage.sprite = mutedSprite;
            PlayerPrefManager.SetMuteState(true);

        }
        else
        {
            muteText = "unmuted";
            muteImage.sprite = unmutedSprite;
            PlayerPrefManager.SetMuteState(false);
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
