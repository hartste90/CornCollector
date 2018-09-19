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

	public Text recentCoinCountText;
    public Text bestCoinCountText;
    public Text pinkCoinCountText;
    public Text totalCoinCountText;
    public Text continueCoinCostText;
    public Text numContinuesText;


    public GameController gameController;
    public AdController adController;
    public GameObject gameOverPanel;
    public GameObject storePanel;
    public GameObject goToStorePanel;

    public Button replayButton;
    public Button continueAdButton;
    public Button continueCoinsButton;
    public GameObject continueCoinPanel;

    private int continueCoinCost;
    private int recentCoinCount;
    private int bestCoinCount;
    private int totalCoinCount;

    private Animator goToStoreButtonAnimator;
    private Animator replayButtonAnimator;

    private bool replayButtonIsVisible = false;

    void Awake()
    {
        goToStoreButtonAnimator = continueCoinPanel.GetComponent<Animator>();
        replayButtonAnimator = replayButton.GetComponent<Animator>();
    }

    public void PopulateEndgameScreenContent(string recentCoinCountSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        replayButtonIsVisible = false;
        this.recentCoinCount = System.Int32.Parse(recentCoinCountSet);
        this.bestCoinCount = System.Int32.Parse(bestCoinCountSet);
        this.totalCoinCount = System.Int32.Parse(totalCoinCountSet);
        this.recentCoinCountText.text = recentCoinCountSet;
        this.bestCoinCountText.text = bestCoinCountSet;
        this.totalCoinCountText.text = totalCoinCountSet;
        //calculate how many pink coins were earned and add to user collection
        int pinkCoinsEarned = recentCoinCount / 100;
        recentCoinCount = 0; //TODO: this should animate to 0
        GameModel.AddPinkCoins(pinkCoinsEarned);
        //calculate pink coin total
        this.pinkCoinCountText.text = GameModel.GetPinkCoinCount().ToString();


        this.continueCoinCost = 10;//need to calculate cointinue coin cost   Mathf.Max(200, (System.Int32.Parse(recentCoinCountSet) / 2) * GameModel.numAttempts/10);
        if (GameController.verbose)
        {
            Debug.Log(string.Format("Cost to continue is {0} = RecentCoins ({1}) / 2 ) / 10 * 10) * numAttempts ({2} / 10)", continueCoinCost, recentCoinCount, GameModel.numAttempts));
        }
        this.continueCoinCostText.text = this.continueCoinCost.ToString();

    }

    public void ShowEndGameScreen()
    {
        this.storePanel.SetActive(false);
        this.gameOverPanel.SetActive(true);
        
        
        //TODO animate UI on
        if (adController.IsReady())
        {
            ShowContinueWithAdsOption();
        }
        //else if (this.continueCoinCost > GameModel.GetPinkCoinCount())
        //{
        //    ShowBuyCoinOption();
        //}
        else
        {
            ShowContinueWithCoinsOption();

        }

        ShowReplayButtonAfterSeconds(GameModel.timeDelayReplayButton);
    }

    //shows the panel that allows users to continue the game by completing a rewarded ad
    private void ShowContinueWithAdsOption()
    {
        this.continueAdButton.gameObject.SetActive(true);
        this.continueCoinPanel.gameObject.SetActive(false);
    }
    //shows the panel that allows users to continue by using pink coins, triggers shop if not enough coins currently
    private void ShowContinueWithCoinsOption()
    {
        this.goToStorePanel.SetActive(false);
        this.continueCoinsButton.interactable = true;
        this.continueAdButton.gameObject.SetActive(false);
        this.continueCoinPanel.gameObject.SetActive(true);
        this.goToStoreButtonAnimator.SetTrigger("Show");
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
    }

    public void ShowEndgameFromStore()
    {
        ShowEndGameScreen();


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
        ShowReplayButton();
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

    private void ShowReplayButton()
    {
        replayButtonAnimator.SetTrigger("Show");
        replayButtonIsVisible = true;
    }

}
