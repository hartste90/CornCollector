﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialController : MonoBehaviour {

    public List<JITSafePanelController> tipList;

    private Animator animator;
    private JITSafePanelController currentTip;
    private List<JITSafePanelController> hiddenTipList;
    private List<JITSafePanelController> shownTipList;
    private int lastIndexShown = -1;

    public delegate void InterstitialCompleteCallback();
    public InterstitialCompleteCallback completeCallback;


	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        //TODO: the tips that are hidden and shown should follow a user across devices
        hiddenTipList = new List<JITSafePanelController>();
        shownTipList = new List<JITSafePanelController>();
    }

    public void ShowTip()
    {
        //choose tip to show
        currentTip = GetTipToShow();
        if (currentTip == null){
            completeCallback();
            return;
        }
        //show the tip and the tip panel
        currentTip.Show();
        animator.SetTrigger("Show");
    }

    private JITSafePanelController GetTipToShow()
    {
        //if we've shown all the tips, check the shown tip list
        if (tipList.Count <= 0)
        {
            //if there are no shown tips (all have been hidden) return null
            if (shownTipList.Count <= 0)
            {
                return null;
            }
            //otherwise return a random tip from the shown list
            return shownTipList[Random.Range(0, shownTipList.Count)];
        }
        JITSafePanelController tip = tipList[0];
        shownTipList.Add(tip);
        tipList.Remove(tip);
        return tip;

    }

    public void ShowRandomTip()
    {
        //choose index
        //TODO: account for empty tip list (all tips hidden)
        int index = Random.Range(0, tipList.Count+1);
        //show tip
        ShowTipByIndex(index);

    }

    private void ShowTipByIndex(int index)
    {
        tipList[index].Show();
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");

    }

    public void HandleInterstitialButtonPressed()
    {
        Hide();
    }

    public void HandleInterstitialDontShowAgainButtonPressed()
    {
        //TODO: mark this tip as hidden (won't be shown in random tips)
        hiddenTipList.Add(currentTip);
        tipList.Remove(currentTip);
        Hide();
    }

    public void OnHideAnimationComplete()
    {
        currentTip.Hide();
        currentTip = null;
        completeCallback();
    }
}
