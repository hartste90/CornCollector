

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

    public Transform pinkCoinTransform;
    public Transform purchasedCoinTransform;
    public GameObject purchasedCoinPrefab;
    private GameObject purchasedCoinPackage;

    private CanvasGroup canvasGroup;
    private Animator animator;

    private bool isShowingPurchaseAnimation = false;

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
    public void ShowWithPurchase(int numCoins)
    {
        rollupController.Hide();
        optionsPanelController.Hide();
        EnterLeft();
        ShowCoinPurchaseAnimation(numCoins);
    }

    private void ShowCoinPurchaseAnimation(int numCoins)
    {
        //create purchased coin prefab
        purchasedCoinPackage = Instantiate(purchasedCoinPrefab, purchasedCoinTransform);
        purchasedCoinPackage.transform.localPosition = Vector3.zero;
        purchasedCoinPackage.GetComponent<PurchasedCoinController>().Populate(pinkCoinTransform, numCoins);
    }

    public void OnPurchaseAnimationOver()
    {
        ShowAfterPurchase();
        Destroy(purchasedCoinPackage);
        purchasedCoinPackage = null;
    }

    public void ShowAfterPurchase()
    {
        //rollupController.ShowAllPanels();
        optionsPanelController.ShowPanelsForPurchase();
        //GetComponent<Animator>().SetTrigger("Show");
    }

    public void ShowContinueWithCoinsSmall(bool shouldShowImmediately)
    {
        this.optionsPanelController.ShowContinueWithCoinsSmall(shouldShowImmediately);
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

        rollupController.hideStatsPanelAnimationCompleteCallback = endgameScreenController.OnContinueGame;

    }

    public void OnShowStoreFromEndgame()
    {
        jitEndScreenController.HideStorePanel();
        endgameScreenController.ShowStoreFromEndgame();
    }

    public void ShowEndGameScreen(bool shouldRollup)
    {
        EnterRight();
    }

    public void OnEndScreenEnterLeftAnimationComplete()
    {
        EnablePanelInput();
        if (purchasedCoinPackage == null) //pressed the back button
        {
            optionsPanelController.Show(rollupController.GetGoldForRound(), true);
        }
        rollupController.Show(rollupController.GetGoldForRound());
    }

    public void OnEndScreenEnterRightAnimationComplete()
    {
        EnablePanelInput();
        int goldForRound = rollupController.GetGoldForRound();

        rollupController.Show(goldForRound);

        optionsPanelController.Show(goldForRound, false);


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

    //public void OnEndScreenExitLeftAnimationComplete()
    //{
    //    OnEndScreenExitAnimationComplete();
    //}
    public void OnEndScreenExitRightAnimationComplete()
    {
        OnEndScreenExitAnimationComplete();
        jitEndScreenController.HideSafePanel();
    }

    public void OnEndScreenExitAnimationComplete()
    {
        DisablePanelInput();
        endgameScreenController.HandleEndScreenOffAnimationComplete();

    }

    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }
    public void EnterRight()
    {
        animator.SetTrigger("EnterRight");
    }
    public void EnterLeft()
    {
        animator.SetTrigger("EnterLeft");
    }
    public void ExitRight()
    {
        optionsPanelController.Hide();
        animator.SetTrigger("ExitRight");
    }
    public void ExitLeft()
    {
        optionsPanelController.Hide();
        animator.SetTrigger("ExitLeft");
    }

    public void ShowStoreJIT()
    {
        jitEndScreenController.ShowStorePanel();
    }

}
