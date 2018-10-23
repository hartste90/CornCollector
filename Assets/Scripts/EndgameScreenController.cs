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
    public GameOverPanelController gameOverPanelController;
    public HelptextPanelController helpTextController;
    public JITEndscreenController jitEndScreenController;
    public StorePanelController storeController;

    //TODO: move to purchase controller?
    //endgame purchase animation
    public Transform pinkCoinTransform;
    public Transform purchasedCoinTransform;
    public GameObject purchasedCoinPrefab;
    private GameObject purchasedCoinPackage;

    private int gameplayCoinCount;

    public void PopulateEndgameScreenContent(string goldCoinTotalSet, string bestCoinCountSet)
	{
        this.gameOverPanelController.Populate(goldCoinTotalSet, bestCoinCountSet, this);
        this.gameplayCoinCount = System.Int32.Parse(goldCoinTotalSet);
    }

    public void ShowEndGameScreen(bool shouldShowImmediately = false)
    {
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
        storeController.Show();
        gameOverPanelController.Hide();
        helpTextController.ShowHelpText();
        jitEndScreenController.HideCoinPanel(true);
    }

    public void ShowEndgameFromStore()
    {
        storeController.Hide();
        ShowEndGameScreen(true);
        jitEndScreenController.ShowCoinPanel();
    }


    private IEnumerator ShowEndgameWithPurchase(float time, int numCoins)
    {
        storeController.Hide();
        yield return new WaitForSeconds(time);
        //create purchased coin prefab
        purchasedCoinPackage = Instantiate(purchasedCoinPrefab, purchasedCoinTransform);
        purchasedCoinPackage.transform.localPosition = Vector3.zero;
        purchasedCoinPackage.GetComponent<PurchasedCoinController>().Populate(pinkCoinTransform, numCoins);
        this.gameOverPanelController.ShowWithPurchase();
    }

    private void ShowEndGameScreenAfterPurchase()
    {
        this.gameOverPanelController.ShowAfterPurchase();
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
       StartCoroutine(ShowEndgameWithPurchase(0.75f, numCoins));
    }

    public void OnPurchaseAnimationOver()
    {
        ShowEndGameScreenAfterPurchase();
        Destroy(purchasedCoinPackage);
    }

    public void HandleRemoveAdsButtonPressed()
    {
        //purchaseManager.HandleRemoveAdsButtonPressed();
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
        ShowReplayButton(true);
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
