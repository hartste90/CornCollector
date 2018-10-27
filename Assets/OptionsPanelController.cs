using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour {

    public GameOverPanelController gameOverController;
    public StoreButtonController storeButtonController;
    public ContinueButtonController continueButtonController;
    public RateGameController rateGameController;
    public ReplayPanelController replayPanelController;
    public AdController adController;


    public void Populate(int continueCoinCostSet)
    {
        this.continueButtonController.SetCoinCost(continueCoinCostSet);
    }

    public void HandleAdButtonPressed()
    {
        adController.ShowRewardedAd();
    }

    public void Hide()
    {
        storeButtonController.HideImmediate();
        continueButtonController.HideImmediate();
        replayPanelController.HideImmediate();
        rateGameController.HideImmediate();
    }

    public void ShowPanelsForPurchase()
    {
        storeButtonController.Show();
        continueButtonController.Show();
        replayPanelController.Show();
    }

    public void ShowContinueWithCoinsOption(bool shouldShowImmediately)
    {
        continueButtonController.ShowAsCoin();
        if (shouldShowImmediately)
        {
            continueButtonController.ShowImmediate();
        }
        else
        {
            continueButtonController.Show();
        }
    }

    public void ShowContinueWithCoinsSmall(bool shouldShowImmediately)
    {
        continueButtonController.ShowAsCoin();
        this.continueButtonController.ShowSmall(shouldShowImmediately);

    }


    public void ShowContinueWithAdsOption()
    {
        continueButtonController.ShowAsAd();
        this.continueButtonController.Show();
    }

    public void ShowReplayButton(bool shouldShowImmediately = false)
    {
        if (shouldShowImmediately)
        {
            replayPanelController.ShowImmediate();
        }
        else
        {
            replayPanelController.Show();
        }
    }

    public void ShowReplayButtonAfterSeconds(float seconds)
    {
        StartCoroutine(ShowReplayButtonAfterTime(seconds));
    }

    public IEnumerator ShowReplayButtonAfterTime(float time)
    {
            yield return new WaitForSeconds(time);
            ShowReplayButton();
    }



    public void OnContinueGame()
    {
        
        gameOverController.OnContinueGame();
    }

    public void OnShowStoreFromEndgame()
    {
        gameOverController.OnShowStoreFromEndgame();
    }

    public void HideContinueButton(bool shouldHideImmediately)
    {
        continueButtonController.SetInteractable(false);
        if (shouldHideImmediately)
        {
            continueButtonController.HideImmediate();
        }
        else
        {
            continueButtonController.Hide();
        }

    }

    public void Show(int goldForRound, bool shouldShowImmediately)
    {
        if (goldForRound <= 20)
        {
            HideContinueButton(true);
        }
        else
        {
            storeButtonController.Show();
            if (adController.IsReady())
            {
                ShowContinueWithAdsOption();
            }
            else
            {
                if (ShouldAskForRating(goldForRound))
                {
                    ShowContinueWithCoinsSmall(shouldShowImmediately);
                    rateGameController.ShowRateGamePanel();
                    rateGameController.ShowPrimaryQuestionPanel();
                }
                else
                {
                    ShowContinueWithCoinsOption(shouldShowImmediately);
                }

            }
        }
        if (shouldShowImmediately || goldForRound <= 20)
        {
            ShowReplayButton(true);
        }
        else
        {
            ShowReplayButtonAfterSeconds(GameModel.timeDelayReplayButton);
        }
    }

    private bool ShouldAskForRating(int goldForRound)
    {
        DateTime currentDate = DateTime.Now;
        Hashtable firstLoginDate = PlayerPrefManager.GetFirstLoginDate();
        if (
            (PlayerPrefManager.GetNumLogins() > 5) &&
            ((int)firstLoginDate["year"] < currentDate.Year && (int)firstLoginDate["day"] < currentDate.DayOfYear) &&
            (PlayerPrefManager.GetBestScore() >= 200 || PlayerPrefManager.GetBestScore() <= goldForRound))
        {
            Debug.Log("display_rating_panel");
            return true;
        }
        return false;
    }

    public void ShowStoreJIT()
    {
        gameOverController.ShowStoreJIT();
    }
}
