using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialController : MonoBehaviour {

    public JITSafePanelController safeTipController;
    public JITSafePanelController wraparoundTipController;

    private Animator animator;


    public delegate void InterstitialCompleteCallback();
    public InterstitialCompleteCallback completeCallback;


	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
	}

    public void ShowRandomTip()
    {
        int tipIndex = Random.Range(0, 2);
        switch(tipIndex)
        {
            case 0:
                ShowSafeTip();
                break;
            case 1:
                ShowWraparoundTip();
                break;
            default:
                ShowSafeTip();
                break;
        }

    }
	
    public void ShowSafeTip()
    {
        animator.SetTrigger("Show");
        safeTipController.Show();
    }

    public void ShowWraparoundTip()
    {
        animator.SetTrigger("Show");
        wraparoundTipController.Show();
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");

    }

    public void HandleInterstitialButtonPressed()
    {
        Hide();
    }

    public void OnHideAnimationComplete()
    {
        safeTipController.Hide();
        wraparoundTipController.Hide();
        completeCallback();
    }
}
