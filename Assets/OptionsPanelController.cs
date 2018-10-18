using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelController : MonoBehaviour {

    public Animator continueAdButtonAnimator;
    public Animator continueCoinButtonAnimator;
    public Animator replayButtonAnimator;
    public Animator rateGamePanelAnimator;


    public void HidePanelsForPurchase()
    {
        continueAdButtonAnimator.SetTrigger("HideImmediate");
        continueCoinButtonAnimator.SetTrigger("HideImmediate");
        replayButtonAnimator.SetTrigger("HideImmediate");
        rateGamePanelAnimator.SetTrigger("HideImmediate");
    }

    public void ShowPanelsForPurchase()
    {
        //continueAdButtonAnimator.SetActive(true);
        continueCoinButtonAnimator.SetTrigger("Show");
        replayButtonAnimator.SetTrigger("Show");
        //rateGamePanelAnimator.SetTrigger("Show");
    }
}
