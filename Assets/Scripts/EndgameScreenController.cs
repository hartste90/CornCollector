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



    public Text bestCoinCountText;
    public Text continueCoinCostText;
    public Text numContinuesText;


    public GameController gameController;
    public AdController adController;
    public RollupController rollupController;
    public HelptextPanelController helpTextController;
    public JITEndscreenController jitEndScreenController;
    public RateGameController rateGameController;
    public GameObject gameOverPanel;
    public GameObject storePanel;
    public GameObject goToStorePanel;

    public Button replayButton;
    public Button continueAdButton;
    public Button continueCoinsButton;
    public GameObject continueCoinPanel;

    private int continueCoinCost;
    private int bestCoinCount;
    private int gameplayCoinCount;

    private Animator goToStoreButtonAnimator;
    private Animator replayButtonAnimator;


    private bool replayButtonIsVisible = false;

    void Awake()
    {
        goToStoreButtonAnimator = continueCoinPanel.GetComponent<Animator>();
        replayButtonAnimator = replayButton.GetComponent<Animator>();
    }



    public void PopulateEndgameScreenContent(string goldCoinTotalSet, string bestCoinCountSet)
	{
        rollupController.Populate(System.Int32.Parse(goldCoinTotalSet), this);
        replayButtonIsVisible = false;
        this.gameplayCoinCount = System.Int32.Parse(goldCoinTotalSet);
        this.bestCoinCount = System.Int32.Parse(bestCoinCountSet);
        this.continueCoinCost = 10;//need to calculate cointinue coin cost   Mathf.Max(200, (System.Int32.Parse(goldCoinTotalSet) / 2) * GameModel.numAttempts/10);

        this.bestCoinCountText.text = bestCoinCountSet;
        this.continueCoinCostText.text = this.continueCoinCost.ToString();

    }

    public void ShowEndGameScreen(bool shouldShowImmediately = false)
    {
        Time.timeScale = 1f;
        this.storePanel.SetActive(false);
        this.gameOverPanel.SetActive(true);

        rollupController.ShowDetails();  //TODO: only show rollup if > 100, dont just remove gold coins it doesnt make sense to a new player


        if (this.gameplayCoinCount <= 20)
        {
            jitEndScreenController.ShowSafePanel();
            HideAllContinueButtons();
        }
        else
        {
            if (this.gameplayCoinCount < 100)
            {
                jitEndScreenController.ShowCoinPanel();
            }
            if (adController.IsReady())
            {
                ShowContinueWithAdsOption();
            }
            else
            {
                if (ShouldAskForRating())
                {
                    ShowContinueWithCoinsSmall(shouldShowImmediately);
                    rateGameController.ShowRateGamePanel();
                    rateGameController.ShowPrimaryQuestionPanel();
                }
                else{
                    ShowContinueWithCoinsOption(shouldShowImmediately);
                }

            }
        }
        if (shouldShowImmediately || this.gameplayCoinCount <= 20)
        {
            ShowReplayButton(true);
        }
        else
        {
            ShowReplayButtonAfterSeconds(GameModel.timeDelayReplayButton);
        }
    }

    private bool ShouldAskForRating()
    {
        return true;
        DateTime currentDate = DateTime.Now;
        Hashtable firstLoginDate = PlayerPrefManager.GetFirstLoginDate();
        if (
            (PlayerPrefManager.GetNumLogins() > 5) &&
            ((int)firstLoginDate["year"] < currentDate.Year && (int)firstLoginDate["day"] < currentDate.DayOfYear) &&
            (bestCoinCount >= 200 || bestCoinCount <= gameplayCoinCount))
        {
            Debug.Log("display_rating_panel");
            return true;
        }
        return false;
    }



    private void HideAllContinueButtons()
    {
        goToStoreButtonAnimator.SetTrigger("HideImmediate");

    }

    public void OnCoinRollupComplete()
    {
        //Debug.Log("Coin rollup animation complete");
    }

    //shows the panel that allows users to continue the game by completing a rewarded ad
    private void ShowContinueWithAdsOption()
    {
        this.continueAdButton.gameObject.SetActive(true);
        this.continueCoinPanel.gameObject.SetActive(false);
    }
    //shows the panel that allows users to continue by using pink coins, triggers shop if not enough coins currently
    private void ShowContinueWithCoinsOption(bool shouldShowImmediately)
    {


    }

    private void ShowContinueWithCoinsSmall(bool shouldShowImmediately)
    {
        this.goToStorePanel.SetActive(false);
        this.continueCoinsButton.interactable = true;
        this.continueAdButton.gameObject.SetActive(false);
        this.continueCoinPanel.gameObject.SetActive(true);
        if (shouldShowImmediately)
        {
            this.goToStoreButtonAnimator.SetTrigger("ShowSmallImmediate");
        }
        else
        {
            this.goToStoreButtonAnimator.SetTrigger("ShowSmall");
        }
    }

    public void HandleContinueAdButtonPressed()
    {
        adController.ShowRewardedAd();
        GameModel.numAttempts++;
    }

    public void HandleContinueCoinButtonPressed()
    {
        //if have enough coins already, take away coins and continue game
        if (PlayerPrefManager.GetPinkCount() >= this.continueCoinCost)
        {
            gameController.ContinueGame();
            PlayerPrefManager.SubtractPinkCoins(this.continueCoinCost);
            GameModel.numAttempts++;
        }
        //otherwise show the shop screen to buy those coins
        else
        {
            ShowStoreFromEndgame();
        }
    }

    public void ShowStoreFromEndgame()
    {
        storePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        helpTextController.ShowHelpText();
        jitEndScreenController.HideCoinPanel(true);
    }

    public void ShowEndgameFromStore()
    {
        ShowEndGameScreen(true);
        jitEndScreenController.ShowCoinPanel();


    }

    public void HandleBuyCoinButtonPressed(int packageId)
    {
        IAPManager.PurchasePackage(packageId);
    }

    public void HandleRemoveAdsButtonPressed()
    {
        IAPManager.HandleRemoveAdsButtonPressed();
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
        StartCoroutine(ShowReplayButtonAfterTime(seconds));
    }

    public IEnumerator ShowReplayButtonAfterTime(float time)
    {

        if (replayButtonIsVisible == false)
        {
            yield return new WaitForSeconds(time);
            ShowReplayButton();
        }
    }

    private void ShowReplayButton(bool shouldShowImmediately = false)
    {
        if (shouldShowImmediately)
        {
            replayButtonAnimator.SetTrigger("ShowImmediate");
        }
        else
        {
            replayButtonAnimator.SetTrigger("Show");
        }
        replayButtonIsVisible = true;
    }

}
