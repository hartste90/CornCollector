

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{

    public EndgameScreenController endgameScreenController;
    public RollupController rollupController;
    public OptionsPanelController optionsPanelController;
    public RateGameController rateGameController;
    public JITEndscreenController jitEndScreenController;

    private CanvasGroup canvasGroup;
    private Animator animator;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    public void EnablePanelInput()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisablePanelInput()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

    }

    //just shows the pink coin, without the options panel
    public void ShowWithPurchase()
    {
        rollupController.Hide();
        optionsPanelController.Hide();
        GetComponent<Animator>().SetTrigger("Show");
    }

    public void ShowAfterPurchase()
    {
        //Debug.Break();
        rollupController.ShowAllPanels();
        optionsPanelController.ShowPanelsForPurchase();
        GetComponent<Animator>().SetTrigger("Show");

    }

    public void ShowContinueWithCoinsSmall(bool shouldShowImmediately)
    {
        this.optionsPanelController.ShowContinueWithCoinsSmall(shouldShowImmediately);
    }

    public void ShowContinueWithAdsOption()
    {
        this.optionsPanelController.ShowContinueWithAdsOption();
    }

    public void ShowReplayButton(bool shouldShowImmediately = false)
    {
        this.optionsPanelController.ShowReplayButton(shouldShowImmediately);
    }

    public void Populate(string goldCoinTotalSet, string bestCoinCountSet, EndgameScreenController endgameScreenController)
    {
        rollupController.Populate(System.Int32.Parse(goldCoinTotalSet), bestCoinCountSet, endgameScreenController);
        int continueCoinCost = 10;//need to calculate cointinue coin cost   Mathf.Max(200, (System.Int32.Parse(goldCoinTotalSet) / 2) * GameModel.numAttempts/10);
        this.optionsPanelController.Populate(continueCoinCost);

    }

    public void ShowReplayButtonAfterSeconds(float seconds)
    {
        this.optionsPanelController.ShowReplayButtonAfterSeconds(seconds);
    }

    public void ShowContinueWithCoinsOption(bool shouldShowImmediately)
    {
        this.optionsPanelController.ShowContinueWithCoinsOption(shouldShowImmediately);
    }

    public void OnContinueGame()
    {
        rollupController.Hide();
        optionsPanelController.Hide();
        jitEndScreenController.HideStorePanel();
        jitEndScreenController.HideSafePanel();
        jitEndScreenController.HideCoinPanel();
        endgameScreenController.OnContinueGame();
    }

    public void OnShowStoreFromEndgame()
    {
        endgameScreenController.ShowStoreFromEndgame();
    }

    public void ShowEndGameScreen(bool shouldShowImmediately)
    {
        int goldForRound = rollupController.GetGoldForRound();
        GetComponent<Animator>().SetTrigger("Show");

        rollupController.Show(goldForRound);

        optionsPanelController.Show(goldForRound, shouldShowImmediately);


        if (goldForRound <= 20)
        {
            jitEndScreenController.ShowSafePanel();
        }
        else
        {
            if (goldForRound < 100)
            {
                jitEndScreenController.ShowCoinPanel();
            }
        }
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    public void ShowStoreJIT()
    {
        jitEndScreenController.ShowStorePanel();
    }

}
