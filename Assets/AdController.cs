using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour
{

    public GameController gameController;

    public bool IsReady()
    {
        if(gameController.debugAllowAds)
        {
            return Advertisement.IsReady("rewardedVideo");
        }
        return false;
    }

    public void ShowRewardedAd()
    {
        if (IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                gameController.ContinueGame();
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.Log("The ad failed to be shown.");
                break;
        }
    }
}