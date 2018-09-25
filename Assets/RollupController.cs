using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollupController : MonoBehaviour {

    public float coinCountDelayTime = 1f;
    public int rollupIncrement = 10;



    public GameObject uiRollupCoinPrefab;
    public Transform coinStartTransform;
    public Transform coinEndTransform;
    public Text goldCoinTotalText;
    public Text pinkCoinCountText;

    private int pinkCoinsTotal;
    private int pinkCoinsCurrent;
    private int goldCoinTotal;
    private int goldCoinCurrent;
    private int goldCoinsTransferred;

    private EndgameScreenController endgameScreenController;
    private float coinCountDelayUntilTime;


    private void Update()
    {
        //continue animating the coins counting if they havent finished
        if (Time.time > coinCountDelayUntilTime)
        {
            if (goldCoinCurrent > 0)
            {
                if (goldCoinCurrent >= rollupIncrement)
                {
                    goldCoinCurrent -= rollupIncrement;
                    goldCoinsTransferred += rollupIncrement; 
                }
                else
                {
                    goldCoinsTransferred += goldCoinCurrent;
                    goldCoinCurrent = 0;
                }
                if (goldCoinsTransferred >= 100)
                {
                    goldCoinsTransferred -= 100;
                    pinkCoinsCurrent++;
                    pinkCoinCountText.text = pinkCoinsCurrent.ToString();

                    GameObject pinkCoin = Instantiate(uiRollupCoinPrefab, coinStartTransform);
                    pinkCoin.GetComponent<GravitateToTarget>().SetTarget(coinEndTransform);
                    pinkCoin.transform.localPosition = Vector3.zero;
                    coinStartTransform.GetComponent<Animator>().SetTrigger("Bump");
                    //Debug.Break();
                }
                if (goldCoinCurrent <= 0)
                {
                    goldCoinCurrent = 0;
                    endgameScreenController.OnCoinRollupComplete();
                }
                goldCoinTotalText.text = goldCoinCurrent.ToString();
            }
        }
    }

    public void Populate(int goldCoinTotalSet, EndgameScreenController endgameScreenControllerSet)
    {
        this.endgameScreenController = endgameScreenControllerSet;
        this.goldCoinTotal = goldCoinTotalSet;
        this.goldCoinCurrent = goldCoinTotalSet;

        //calculate how many pink coins were earned and add to user collection
        int pinkCoinsEarned = this.goldCoinTotal / 100;
        //calculate pink coin total
        this.pinkCoinsCurrent = PlayerPrefManager.GetPinkCount();
        PlayerPrefManager.AddPinkCoins(pinkCoinsEarned);
        this.pinkCoinsTotal = PlayerPrefManager.GetPinkCount();

        this.goldCoinTotalText.text = this.goldCoinCurrent.ToString();
        this.pinkCoinCountText.text = this.pinkCoinsCurrent.ToString();

    }

    public void ShowDetails()
    {
        coinCountDelayUntilTime = Time.time + coinCountDelayTime;
        goldCoinsTransferred = 0;
    }

}
