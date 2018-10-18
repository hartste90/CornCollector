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

    public GameOverPanelController gameOverPanelController;
    
    public GameController gameController;
    public IAPManager purchaseManager;
    public AdController adController;
    public RollupController rollupController;
    public HelptextPanelController helpTextController;
    public JITEndscreenController jitEndScreenController;
    public RateGameController rateGameController;
    public GameObject gameOverPanel;
    public GameObject storePanel;
    public GameObject goToStorePanel;
    public Transform pinkCoinTransform;
    public Transform purchasedCoinTransform;
    public GameObject purchasedCoinPrefab;

    public Button replayButton;
    public Button continueAdButton;
    public Button continueCoinsButton;
    public GameObject continueCoinPanel;

    private int continueCoinCost;
    private int bestCoinCount;
    private int gameplayCoinCount;

    private Animator continueAdButtonAnimator;
    private Animator goToStoreButtonAnimator;
    private Animator replayButtonAnimator;

    private GameObject purchasedCoinPackage;

    private bool replayButtonIsVisible = false;

    void Awake()
    {
        continueAdButtonAnimator = continueAdButton.GetComponent<Animator>();
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
        this.gameOverPanel.GetComponent<Animator>().SetTrigger("Show");

        if (ShouldRollup())
        {
            rollupController.StartRollup();  //TODO: only show rollup if > 100, dont just remove gold coins it doesnt make sense to a new player
        }
        else
        {
            rollupController.Show();   
        }




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

    private bool ShouldRollup()
    {
        return gameplayCoinCount >= 100 ? true : false;
    }

    private bool ShouldAskForRating()
    {
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
        this.continueAdButtonAnimator.SetTrigger("Show");
    }
    //shows the panel that allows users to continue by using pink coins, triggers shop if not enough coins currently
    private void ShowContinueWithCoinsOption(bool shouldShowImmediately)
    {
        this.goToStorePanel.SetActive(false);
        this.continueCoinsButton.interactable = true;
        this.continueCoinPanel.gameObject.SetActive(true);
        if (shouldShowImmediately)
        {
            this.goToStoreButtonAnimator.SetTrigger("ShowImmediate");
        }
        else
        {
            this.goToStoreButtonAnimator.SetTrigger("Show");
        }

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
            PlayerPrefManager.SubtractPinkCoins(this.continueCoinCost);
            Debug.Log("New pink coin count: " + PlayerPrefManager.GetPinkCount());
            GameModel.numAttempts++;
            gameController.ContinueGame();
        }
        //otherwise show the shop screen to buy those coins
        else
        {
            ShowStoreFromEndgame();
        }
    }

    public void ShowStoreFromEndgame()
    {
        this.storePanel.GetComponent<Animator>().SetTrigger("Show");

        //storePanel.SetActive(true);
        this.gameOverPanel.GetComponent<Animator>().SetTrigger("Hide");
        helpTextController.ShowHelpText();
        jitEndScreenController.HideCoinPanel(true);
        HideCoinJIT();
    }

    public void HideCoinJIT()
    {
        jitEndScreenController.HideCoinPanel(true);
    }

    public void ShowEndgameFromStore()
    {
        this.storePanel.GetComponent<Animator>().SetTrigger("Hide");
        ShowEndGameScreen(true);
        jitEndScreenController.ShowCoinPanel();
    }


    private IEnumerator ShowEndgameWithPurchase(float time, int numCoins)
    {
        this.storePanel.GetComponent<Animator>().SetTrigger("Hide");
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
        replayButton.gameObject.SetActive(true);
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
