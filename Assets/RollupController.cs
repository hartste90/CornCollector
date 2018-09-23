using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollupController : MonoBehaviour {

    public float coinCountDelayTime = .5f;
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

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        //continue animating the coins counting if they havent finished
        if (Time.time > coinCountDelayUntilTime)
        {
            if (goldCoinCurrent > 0)
            {
                goldCoinCurrent -= rollupIncrement;
                goldCoinsTransferred += rollupIncrement;
                if (goldCoinsTransferred >= 100)
                {
                    goldCoinsTransferred -= 100;
                    pinkCoinsCurrent++;
                    pinkCoinCountText.text = pinkCoinsCurrent.ToString();

                    GameObject pinkCoin = Instantiate(uiRollupCoinPrefab, coinStartTransform);
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

    public void Populate(int goldCoinTotalSet, int pinkCoinBefore, EndgameScreenController endgameScreenControllerSet)
    {
        this.endgameScreenController = endgameScreenControllerSet;
        goldCoinTotal = goldCoinTotalSet;
        goldCoinCurrent = goldCoinTotal;

        //calculate how many pink coins were earned and add to user collection
        goldCoinTotal = 0;
        int pinkCoinsEarned = goldCoinTotal / 100;
        GameModel.AddPinkCoins(pinkCoinsEarned);
        //calculate pink coin total
        this.pinkCoinsTotal = GameModel.GetPinkCoinCount();
        this.pinkCoinsCurrent = 0;

        this.goldCoinTotalText.text = goldCoinCurrent.ToString();
        this.pinkCoinCountText.text = 0.ToString();

    }

    public void ShowDetails()
    {
        coinCountDelayUntilTime = Time.time + coinCountDelayTime;
        goldCoinsTransferred = 0;
    }

}
