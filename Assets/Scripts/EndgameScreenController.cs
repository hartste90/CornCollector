using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;


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
    public GameObject gameOverPanel;
    public GameObject storePanel;
    public GameObject goToStorePanel;

    public Button replayButton;
    public Button continueAdButton;
    public Button continueCoinsButton;
    public GameObject continueCoinPanel;

    private int continueCoinCost;
    private int bestCoinCount;

    private Animator goToStoreButtonAnimator;
    private Animator replayButtonAnimator;


    private bool replayButtonIsVisible = false;

    void Awake()
    {
        goToStoreButtonAnimator = continueCoinPanel.GetComponent<Animator>();
        replayButtonAnimator = replayButton.GetComponent<Animator>();
    }



    public void PopulateEndgameScreenContent(string goldCoinTotalSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        rollupController.Populate(System.Int32.Parse(goldCoinTotalSet), GameModel.GetPinkCoinCount(), this);
        replayButtonIsVisible = false;
        this.bestCoinCount = System.Int32.Parse(bestCoinCountSet);
        this.continueCoinCost = 10;//need to calculate cointinue coin cost   Mathf.Max(200, (System.Int32.Parse(goldCoinTotalSet) / 2) * GameModel.numAttempts/10);

        this.bestCoinCountText.text = bestCoinCountSet;
        this.continueCoinCostText.text = this.continueCoinCost.ToString();

    }

    public void ShowEndGameScreen(bool shouldShowImmediately = false)
    {
        this.storePanel.SetActive(false);
        this.gameOverPanel.SetActive(true);

        rollupController.ShowDetails();
        
        //TODO animate UI on
        if (adController.IsReady())
        {
            ShowContinueWithAdsOption();
        }
        else
        {
            ShowContinueWithCoinsOption(shouldShowImmediately);

        }

        if (shouldShowImmediately)
        {
            ShowReplayButton(true);
        }
        else
        {
            ShowReplayButtonAfterSeconds(GameModel.timeDelayReplayButton);
        }

    }

    public void OnCoinRollupComplete()
    {
        Debug.Log("Coin rollup animation complete");
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
        this.goToStorePanel.SetActive(false);
        this.continueCoinsButton.interactable = true;
        this.continueAdButton.gameObject.SetActive(false);
        this.continueCoinPanel.gameObject.SetActive(true);
        if(shouldShowImmediately)
        {
            this.goToStoreButtonAnimator.SetTrigger("ShowImmediate");
        }
        else{
            this.goToStoreButtonAnimator.SetTrigger("Show");
        }

    }

    //private void ShowBuyCoinOption()
    //{
    //    this.goToStorePanel.SetActive(true);
    //    this.continueCoinsButton.interactable = false;
    //    this.goToStoreButtonAnimator.SetTrigger("Show");
    //}


    public void HandleContinueAdButtonPressed()
    {
        adController.ShowRewardedAd();
        GameModel.numAttempts++;
    }

    public void HandleContinueCoinButtonPressed()
    {
        //if have enough coins already, take away coins and continue game
        if (GameModel.GetPinkCoinCount() > this.continueCoinCost)
        {
            gameController.ContinueGame();
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
    }

    public void ShowEndgameFromStore()
    {
        ShowEndGameScreen(true);


    }

    public void HandleBuyCoinButtonPressed(int packageId)
    {
        IAPManager.PurchasePackage(packageId);
    }

    public void HandleRemoveAdsButtonPressed()
    {
        IAPManager.HandleRemoveAdsButtonPressed();
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
