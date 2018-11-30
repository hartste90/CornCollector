using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollupController : MonoBehaviour {

    public float coinCountDelayTime = 1f;
    public int rollupIncrement = 10;


    public PinkCoinPanelController pinkCoinPanelController;
    public GoldCoinPanelController goldCoinPanelController;

    public GameObject uiRollupCoinPrefab;
    public Transform coinStartTransform;
    public Transform coinEndTransform;
    public Text goldCoinTotalText;
    public Text pinkCoinCountText;
    public Text bestGoldLabel;
    public Text bestGoldText;

    public Animator pinkPanelAnimator;
    public Animator goldPanelAnimator;
    public Animator bestPanelAnimator;

    private int pinkCoinsTotal;
    private int pinkCoinsCurrent;
    private int goldCoinTotal;
    private int goldCoinCurrent;
    private int goldCoinsTransferred;

    private EndgameScreenController endgameScreenController;
    private float coinCountDelayUntilTime;
    private bool shouldRollUp = false;

    public delegate void HideStatsPanelAnimationCompleteCallback();
    public HideStatsPanelAnimationCompleteCallback hideStatsPanelAnimationCompleteCallback;

    private void Update()
    {
        //continue animating the coins counting if they havent finished
        if (Time.time > coinCountDelayUntilTime && shouldRollUp)
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
                    shouldRollUp = false;
                    endgameScreenController.OnCoinRollupComplete();
                }
                goldCoinTotalText.text = goldCoinCurrent.ToString();
            }
        }
    }

    public void Populate(int goldCoinTotalSet, string bestCoinCountSet, EndgameScreenController endgameScreenControllerSet)
    {

        this.bestGoldText.text = bestCoinCountSet;
        
        Debug.Log(this.bestGoldLabel.fontSize);
        Debug.Log(this.bestGoldText.fontSize);
        this.bestGoldText.fontSize = this.bestGoldLabel.cachedTextGenerator.fontSizeUsedForBestFit;


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

    public int GetGoldForRound()
    {
        return goldCoinTotal;
    }

    public void StartRollup()
    {
        shouldRollUp = true;
        coinCountDelayUntilTime = Time.time + coinCountDelayTime;
        goldCoinsTransferred = 0;
    }

    public void ShowImmediate()
    {
        pinkCoinCountText.text = PlayerPrefManager.GetPinkCount().ToString();
    }


    public void Show(int goldForRound)
    {
        ShowAllPanels();
        ShowImmediate();
        return;
        if (ShouldRollup(goldForRound))
        {
            StartRollup();
        }
        else
        {
            ShowImmediate();
        }
    }

    public bool ShouldRollup(int goldForRound)
    {
        return goldForRound >= 100 ? true : false;
    }

    public void Hide()
    {
        goldCoinPanelController.hideCompleteCallback = HandleHideAnimationComplete;
        pinkPanelAnimator.SetTrigger("Hide");
        goldPanelAnimator.SetTrigger("Hide");
        bestPanelAnimator.SetTrigger("Hide");
    }

    public void ShowAllPanels()
    {
        pinkPanelAnimator.SetTrigger("Show");
        goldPanelAnimator.SetTrigger("Show");
        bestPanelAnimator.SetTrigger("Show");
    }

    public void OnCollectPurchaseCoin()
    {
        pinkCoinsCurrent++;
        this.pinkCoinCountText.text = this.pinkCoinsCurrent.ToString();
    }

    public void HandleHideAnimationComplete()
    {
        if (hideStatsPanelAnimationCompleteCallback != null)
        {
            hideStatsPanelAnimationCompleteCallback();
            hideStatsPanelAnimationCompleteCallback = null;
        }

    }
}
