using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class EndgameScreenController : MonoBehaviour {

	public Text recentCoinCount;
    public Text bestCoinCount;
    public Text totalCoinCount;

    public AdController adController;

    public Button continueButton;

    public void PopulateEndgameScreenContent(string recentCoinCountSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        this.recentCoinCount.text = recentCoinCountSet;
        this.bestCoinCount.text = bestCoinCountSet;
        this.totalCoinCount.text = totalCoinCountSet;
        if (adController.IsReady())
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    public void HandleContinueButtonPressed()
    {
        adController.ShowRewardedAd();
    }
}
