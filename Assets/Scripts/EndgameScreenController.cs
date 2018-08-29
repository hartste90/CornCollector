using UnityEngine;
using UnityEngine.UI;

public class EndgameScreenController : MonoBehaviour {

	public Text recentCoinCount;
    public Text bestCoinCount;
    public Text totalCoinCount;

    public void PopulateEndgameScreenContent(string recentCoinCountSet, string bestCoinCountSet, string totalCoinCountSet)
	{
        this.recentCoinCount.text = recentCoinCountSet;
        this.bestCoinCount.text = bestCoinCountSet;
        this.totalCoinCount.text = totalCoinCountSet;

	}
}
