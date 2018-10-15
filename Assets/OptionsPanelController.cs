using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelController : MonoBehaviour {

    public GameObject continueAdButton;
    public Animator continueCoinButtonAnimator;
    public Animator replayButtonAnimator;
    public Animator rateGamePanelAnimator;


    public void HidePanelsForPurchase()
    {
        continueAdButton.SetActive(false);
        continueCoinButtonAnimator.SetTrigger("HideImmediate");
        replayButtonAnimator.SetTrigger("HideImmediate");
        rateGamePanelAnimator.SetTrigger("HideImmediate");
    }

    public void ShowPanelsForPurchase()
    {
        //continueAdButton.SetActive(true);
        continueCoinButtonAnimator.SetTrigger("Show");
        replayButtonAnimator.SetTrigger("Show");
        //rateGamePanelAnimator.SetTrigger("Show");
    }
}
