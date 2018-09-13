using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class EndgameScreenController : MonoBehaviour {

	public Text recentCoinCountText;
    public Text bestCoinCountText;
    public Text totalCoinCountText;
    public Text continueCoinCostText;
    public Text coinsNeededText;
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

    void Awake()
    {
        goToStoreButtonAnimator = continueCoinPanel.GetComponent<Animator>();
        replayButtonAnimator = replayButton.GetComponent<Animator>(); 

    }

    public void PopulateEndgameScreenContent(string recentCoinCountSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        this.recentCoinCount = System.Int32.Parse(recentCoinCountSet); ;
        this.bestCoinCount = System.Int32.Parse(bestCoinCountSet); ;
        this.totalCoinCount = System.Int32.Parse(totalCoinCountSet);
        this.recentCoinCountText.text = recentCoinCountSet;
        this.bestCoinCountText.text = bestCoinCountSet;
        this.totalCoinCountText.text = totalCoinCountSet;
        this.continueCoinCost = Mathf.Max(200, (((System.Int32.Parse(recentCoinCountSet) / 2) / 10) * 10)) * GameModel.numAttempts;
        this.continueCoinCostText.text = "-"+this.continueCoinCost.ToString();
        //this.numContinuesText.text = GameModel.numAttempts > 1 ? GameModel.numAttempts.ToString() : "";
        this.coinsNeededText.text = (this.continueCoinCost - this.recentCoinCount).ToString();

    }

    public void ShowEndGameScreen()
    {
        this.storePanel.SetActive(false);
        this.gameOverPanel.SetActive(true);
        
        
        //TODO animate UI on
        if (adController.IsReady()) //play an ad
        {
            this.continueAdButton.gameObject.SetActive(true);
            this.continueCoinPanel.gameObject.SetActive(false);
        }
        else if (this.continueCoinCost > this.recentCoinCount) // not enough coins, show buy button
        {
            this.goToStorePanel.SetActive(true);
            this.continueCoinsButton.interactable = false;
            this.goToStoreButtonAnimator.SetTrigger("Show");
        }
        else    //show coin continue button
        {
            this.continueCoinsButton.interactable = true;
            this.continueAdButton.gameObject.SetActive(false);
            this.continueCoinPanel.gameObject.SetActive(true);
        }

        ShowReplayButtonAfterSeconds(GameModel.timeDelayReplayButton);
    }


    public void HandleContinueAdButtonPressed()
    {
        adController.ShowRewardedAd();
        GameModel.numAttempts++;
    }

    public void HandleContinueCoinButtonPressed()
    {
        gameController.ContinueGame(this.continueCoinCost);
        GameModel.numAttempts++;

    }

    public void HandleGetCoinButtonPressed()
    {
        storePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void HandleBuyCoinButtonPressed(int packageId)
    {
        IAPManager.PurchasePackage(packageId);
    }

    public void HandleStartOverButtonPressed()
    {
        gameController.HandleStartOverButtonPressed();
        GameModel.numAttempts = 1;
    }

    public void ShowReplayButtonAfterSeconds(float seconds)
    {
        StartCoroutine(ExecuteAfterTime(seconds));
    }

    public IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        replayButtonAnimator.SetTrigger("Show");
    }

}
