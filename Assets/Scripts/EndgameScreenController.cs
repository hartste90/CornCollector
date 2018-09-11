using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EndgameScreenController : MonoBehaviour {

	public Text recentCoinCountText;
    public Text bestCoinCountText;
    public Text totalCoinCountText;
    public Text continueCoinCostText;
    public Text coinsNeededText;


    public GameController gameController;
    public AdController adController;
    public GameObject gameOverPanel;
    public GameObject storePanel;
    public GameObject goToStorePanel;

    public Button continueAdButton;
    public GameObject continueCoinButton;

    private int continueCoinCost;
    private int recentCoinCount;
    private int bestCoinCount;
    private int totalCoinCount;


    public void PopulateEndgameScreenContent(string recentCoinCountSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        this.recentCoinCount = System.Int32.Parse(recentCoinCountSet); ;
        this.bestCoinCount = System.Int32.Parse(bestCoinCountSet); ;
        this.totalCoinCount = System.Int32.Parse(totalCoinCountSet);
        this.recentCoinCountText.text = recentCoinCountSet;
        this.bestCoinCountText.text = bestCoinCountSet;
        this.totalCoinCountText.text = totalCoinCountSet;
        this.continueCoinCost = Mathf.Max(200, (((System.Int32.Parse(recentCoinCountSet) / 2) / 10) * 10));
        this.continueCoinCostText.text = "-"+this.continueCoinCost.ToString();

        this.coinsNeededText.text = (this.continueCoinCost - this.recentCoinCount).ToString();

    }

    public void ShowEndGameScreen()
    {
        storePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        //TODO animate UI on
        if (adController.IsReady()) //play an ad
        {
            continueAdButton.gameObject.SetActive(true);
            continueCoinButton.gameObject.SetActive(false);
        }
        else if (this.continueCoinCost > this.recentCoinCount) // not enough coins, show buy button
        {
            goToStorePanel.SetActive(true);
        }
        else    //show coin continue button
        {
            continueAdButton.gameObject.SetActive(false);
            continueCoinButton.gameObject.SetActive(true);
        }
    }


    public void HandleContinueAdButtonPressed()
    {
        adController.ShowRewardedAd();
    }

    public void HandleContinueCoinButtonPressed()
    {
        gameController.ContinueGame(this.continueCoinCost);

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
    }
}
