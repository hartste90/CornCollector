using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using System;

public static class Extensions
{
    public static int RoundToTensPlace(this int num)
    {
        return num / 10 * 10;
    }
}

public class EndgameScreenController : MonoBehaviour {


    public GameController gameController;
    public BackgroundOverlayController backgroundOverlayController;
    public GameOverPanelController gameOverPanelController;
    public HelptextPanelController helpTextController;
    public JITEndscreenController jitEndScreenController;
    public StorePanelController storeController;

    public delegate void EndScreenExitCallback();
    public EndScreenExitCallback endScreenExitCallback;

    private int gameplayCoinCount;

    public void Start()
    {
        backgroundOverlayController.HideImmediate();
        //gameObject.SetActive(false);
    }
    public void PopulateEndgameScreenContent(string goldCoinTotalSet, string bestCoinCountSet)
	{
        this.gameOverPanelController.Populate(goldCoinTotalSet, bestCoinCountSet, this);
        this.gameplayCoinCount = System.Int32.Parse(goldCoinTotalSet);
    }

    public void ShowEndGameScreen(bool shouldShowImmediately = false)
    {
        //celebrationController.Celebrate();
        if (!shouldShowImmediately)
        {
            backgroundOverlayController.FadeIn();
        }
        gameOverPanelController.ShowEndGameScreen(shouldShowImmediately);
    }

    public void OnCoinRollupComplete()
    {
        Debug.Log("Coin rollup animation complete");
    }

    //shows the panel that allows users to continue by using pink coins, triggers shop if not enough coins currently
    private void ShowContinueWithCoinsOption(bool shouldShowImmediately)
    {
        this.gameOverPanelController.ShowContinueWithCoinsOption(shouldShowImmediately);
    }

    private void ShowContinueWithCoinsSmall(bool shouldShowImmediately)
    {
        this.gameOverPanelController.ShowContinueWithCoinsSmall(shouldShowImmediately);
    }

    public void ShowStoreFromEndgame()
    {
        storeController.EnterRight();
        gameOverPanelController.ExitLeft();
        //helpTextController.ShowHelpText();
        jitEndScreenController.HideCoinPanel(true);
    }

    public void Hide()
    {
        backgroundOverlayController.FadeOut();
        gameOverPanelController.ExitRight();
        //storeController.ExitRight();
    }

    public void HandleEndScreenOffAnimationComplete()
    {
        if (endScreenExitCallback != null)
        {
            endScreenExitCallback();
            endScreenExitCallback = null;
        }

    }

    public void ShowEndgameFromStore()
    {
        storeController.ExitRight();
        gameOverPanelController.EnterLeft();
    }


    private IEnumerator ShowEndgameWithPurchase(float time, int numCoins)
    {
        storeController.ExitRight();
        yield return new WaitForSeconds(time);
        this.gameOverPanelController.ShowWithPurchase(numCoins);
    }

    //successfully purchased coins
    public void HandleBuyCoinButtonPressed(int numCoinsPurchased)
    {
        if (numCoinsPurchased > 0)
        {
            StartCoinPurchasedAnimation(numCoinsPurchased);
        }
    }

    public void StartCoinPurchasedAnimation(int numCoins)
    {
       StartCoroutine(ShowEndgameWithPurchase(0.0f, numCoins));
    }

    public void HandleRemoveAdsButtonPressed()
    {
        storeController.HandleRemoveAdsPurchased();
    }

    public void HandlePurchaseErrorReceived()
    {
        Debug.LogError("There was an error while purchasing, sent from the app store!");
    }

    public void HandleStartOverButtonPressed()
    {
        gameController.HandleStartOverButtonPressed();
        GameModel.numAttempts = 1;
    }

    public void HandleBackButtonPressed()
    {
        ShowEndgameFromStore();
        //ShowReplayButton(true);
    }

    public void ShowReplayButtonAfterSeconds(float seconds)
    {
        this.gameOverPanelController.ShowReplayButtonAfterSeconds(seconds);
    }

    private void ShowReplayButton(bool shouldShowImmediately = false)
    {
        this.gameOverPanelController.ShowReplayButton(shouldShowImmediately);
    }

    public void OnContinueGame()
    {
        gameController.ContinueGame();
    }

}
