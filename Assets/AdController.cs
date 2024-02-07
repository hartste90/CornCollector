using System.Collections.Generic;
using UnityEngine;

public class AdController : MonoBehaviour
{

    public GameController gameController;

    string gameId = "2983745";
    bool testMode = false;

    void Start()
    {
        Debug.Log("Advertisements Initialized: " + gameId);
    }
    

    public bool IsReady()
    {
        if(gameController.debugAllowAds)
        {
            return false;
            // return Advertisement.IsReady("rewardedVideo");
        }
        return false;
    }

    public void ShowRewardedAd()
    {
        if (IsReady())
        {
            GameModel.numAttempts++;
            // Advertisement.Show("rewardedVideo", options);
        }
    }

}