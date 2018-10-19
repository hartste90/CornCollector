using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour {

    public GameOverPanelController gameOverController;
    public ContinueButtonController continueButtonController;

    public Animator continueAdButtonAnimator;
    public Animator continueCoinButtonAnimator;
    public Animator replayButtonAnimator;
    public Animator rateGamePanelAnimator;

    public GameObject continueCoinPanel;
    public Button continueCoinButton;
    public Button continueAdButton;
    public Button replayButton;



    private bool replayButtonIsVisible = false;


    public void Populate(int continueCoinCostSet)
    {
        this.replayButtonIsVisible = false;
        this.continueButtonController.SetCoinCost(continueCoinCostSet);
    }
    public void HidePanelsForPurchase()
    {
        continueAdButtonAnimator.SetTrigger("HideImmediate");
        continueCoinButtonAnimator.SetTrigger("HideImmediate");
        replayButtonAnimator.SetTrigger("HideImmediate");
        rateGamePanelAnimator.SetTrigger("HideImmediate");
    }

    public void ShowPanelsForPurchase()
    {
        continueCoinButtonAnimator.SetTrigger("Show");
        replayButtonAnimator.SetTrigger("Show");
    }

    public void ShowContinueWithCoinsSmall(bool shouldShowImmediately)
    {

        this.continueCoinButtonAnimator.gameObject.SetActive(false);
        this.continueCoinButton.interactable = true;
        this.continueAdButton.gameObject.SetActive(false);
        this.continueCoinPanel.gameObject.SetActive(true);
        if (shouldShowImmediately)
        {
            this.continueCoinButtonAnimator.SetTrigger("ShowSmallImmediate");
        }
        else
        {
            this.continueCoinButtonAnimator.SetTrigger("ShowSmall");
        }
    }

    public void ShowContinueWithAdsOption()
    {
        this.continueAdButtonAnimator.SetTrigger("Show");
    }

    public void ShowReplayButton(bool shouldShowImmediately = false)
    {
        if (shouldShowImmediately)
        {
            replayButtonAnimator.SetTrigger("ShowImmediate");
        }
        else
        {
            replayButtonAnimator.SetTrigger("Show");
        }
        replayButtonIsVisible = true;
    }

    public void ShowReplayButtonAfterSeconds(float seconds)
    {
        StartCoroutine(ShowReplayButtonAfterTime(seconds));
    }

    public IEnumerator ShowReplayButtonAfterTime(float time)
    {

        if (replayButtonIsVisible == false)
        {
            yield return new WaitForSeconds(time);
            ShowReplayButton();
        }
    }

    public void ShowContinueWithCoinsOption(bool shouldShowImmediately)
    {
        this.continueCoinButton.interactable = true;
        if (shouldShowImmediately)
        {
            this.continueCoinButtonAnimator.SetTrigger("ShowImmediate");
        }
        else
        {
            this.continueCoinButtonAnimator.SetTrigger("Show");
        }
    }

    public void OnContinueGame()
    {
        gameOverController.OnContinueGame();
    }

    public void OnShowStoreFromEndgame()
    {
        gameOverController.OnShowStoreFromEndgame();
    }
}
