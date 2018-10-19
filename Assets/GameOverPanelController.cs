

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{

    public EndgameScreenController endgameScreenController;
    public RollupController rollupController;
    public OptionsPanelController optionsPanelController;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
        rollupController.HidePanelsForPurchase();
        optionsPanelController.HidePanelsForPurchase();
        GetComponent<Animator>().SetTrigger("Show");
    }

    public void ShowAfterPurchase()
    {
        //Debug.Break();
        rollupController.ShowPanelsForPurchase();
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
        endgameScreenController.OnContinueGame();
    }

    public void OnShowStoreFromEndgame()
    {
        endgameScreenController.ShowStoreFromEndgame();
    }
}
