using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JITEndscreenController : MonoBehaviour
{

    public GameObject jitCoinPanel;
    public JITStorePanelController jitStorePanelController;
    public JITSafePanelController jitSafePanelController;

    private Animator jitCoinPanelAnimator;

    private void Awake()
    {
        jitCoinPanel.SetActive(true);
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

    public void ShowStorePanel()
    {
        jitStorePanelController.Show();
    }

    public void HideStorePanel(bool shouldHideImmediately = false)
    {
        if (shouldHideImmediately)
        {
            jitStorePanelController.HideImmediate();
        }
        else
        {
            jitStorePanelController.Hide();
        }
    }
    public void HideSafePanel(bool shouldHideImmediately = false)
    {
        jitSafePanelController.Hide(shouldHideImmediately);
    }


    public void HideCoinPanel (bool shouldHideImmediate = false)
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
