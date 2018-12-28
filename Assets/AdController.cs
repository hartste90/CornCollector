using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

public class AdController : MonoBehaviour
{

    public GameController gameController;

    string gameId = "2983745";
    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Debug.Log("Advertisements Initialized: " + gameId);
    }
    

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
            GameModel.numAttempts++;
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
                Analytics.CustomEvent("adSuccessful", new Dictionary<string, object>
                {
                    { "userId", AnalyticsSessionInfo.userId },
                    { "attempts", GameModel.numAttempts},
                    { "replays", GameModel.numReplays },
                    { "time", Time.time }

                });
                gameController.ContinueGame();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                Analytics.CustomEvent("adSkipped", new Dictionary<string, object>
                {
                    { "userId", AnalyticsSessionInfo.userId },
                    { "attempts", GameModel.numAttempts},
                    { "replays", GameModel.numReplays },
                    { "time", Time.time }

                });
                break;
            case ShowResult.Failed:
                Analytics.CustomEvent("adFailed", new Dictionary<string, object>
                {
                    { "userId", AnalyticsSessionInfo.userId },
                    { "attempts", GameModel.numAttempts},
                    { "replays", GameModel.numReplays },
                    { "time", Time.time }

                });
                Debug.Log("The ad failed to be shown.");
                break;
        }
    }
}