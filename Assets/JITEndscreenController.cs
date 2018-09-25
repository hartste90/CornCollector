using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JITEndscreenController : MonoBehaviour
{

    public GameObject jitCoinPanel;
    public JITSafePanelController jitSafePanelController;

    private Animator jitCoinPanelAnimator;

    private void Awake()
    {
        jitCoinPanelAnimator = jitCoinPanel.GetComponent<Animator>();
    }

    public void ShowCoinPanel()
    {
        jitCoinPanelAnimator.SetTrigger("Show");
    }

    public void ShowSafePanel()
    {
        jitSafePanelController.gameObject.SetActive(true);
        jitSafePanelController.Show();
    }

    public void HideSafePanel(bool shouldHideImmediately)
    {
        jitSafePanelController.Hide(shouldHideImmediately);
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
