using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EndgameScreenController : MonoBehaviour {

	public Text recentCoinCount;
    public Text bestCoinCount;
    public Text totalCoinCount;

    public AdController adController;

    public Button continueAdButton;
    public Button continueCoinButton;


    public void PopulateEndgameScreenContent(string recentCoinCountSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        this.recentCoinCount.text = recentCoinCountSet;
        this.bestCoinCount.text = bestCoinCountSet;
        this.totalCoinCount.text = totalCoinCountSet;
        if (adController.IsReady())
        {
            continueAdButton.gameObject.SetActive(true);
            continueCoinButton.gameObject.SetActive(false);
        }
        else
        {
            continueAdButton.gameObject.SetActive(false);
            continueCoinButton.gameObject.SetActive(true);
        }
    }

    public void HandleContinueButtonPressed()
    {
        adController.ShowRewardedAd();
    }
}
