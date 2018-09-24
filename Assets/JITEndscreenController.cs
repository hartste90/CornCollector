using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JITEndscreenController : MonoBehaviour {

    public GameObject jitCoinPanel;


    private Animator jitCoinPanelAnimator;

    private void Awake()
    {
        jitCoinPanelAnimator = jitCoinPanel.GetComponent<Animator>();
    }

    public void ShowCoinPanel()
    {
        jitCoinPanelAnimator.SetTrigger("Show");
    }

    public void HideCoinPanel (bool shouldHideImmediate)
    {
        if (shouldHideImmediate)
        {
            jitCoinPanelAnimator.SetTrigger("HideImmediate");
        }
        else
        {
            jitCoinPanelAnimator.SetTrigger("Hide");
        }
    }
}
