using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JITEndscreenController : MonoBehaviour
{

    public GameObject jitCoinPanel;
    public JITStorePanelController jitStorePanelController;
    public JITSafePanelController jitSafePanelController;

    private Animator jitCoinPanelAnimator;
    private bool isShowingSafeJIT;
    private bool isShowingStoreJIT;
    private bool isShowingCoinJIT;

    private void Awake()
    {
        jitCoinPanel.SetActive(true);
        jitCoinPanelAnimator = jitCoinPanel.GetComponent<Animator>();
    }

    public void ShowCoinPanel()
    {
        if (!isShowingCoinJIT)
        {
            isShowingCoinJIT = true;
            jitCoinPanelAnimator.SetTrigger("Show");
        }

    }

    public void ShowSafePanel()
    {
        if (!isShowingSafeJIT)
        {
            isShowingSafeJIT = true;
            jitSafePanelController.gameObject.SetActive(true);
            jitSafePanelController.Show();
        }
    }

    public void ShowStorePanel()
    {
        if (!isShowingStoreJIT) 
        {
            isShowingStoreJIT = true;
            jitStorePanelController.Show();
        }

    }

    public void HideStorePanel(bool shouldHideImmediately = false)
    {
        if (isShowingStoreJIT)
        {
            if (shouldHideImmediately)
            {
                jitStorePanelController.HideImmediate();
            }
            else
            {
                jitStorePanelController.Hide();
            }
            isShowingStoreJIT = false;
        }
       
    }
    public void HideSafePanel(bool shouldHideImmediately = false)
    {
        if (isShowingSafeJIT)
        {
            jitSafePanelController.Hide(shouldHideImmediately);
            isShowingSafeJIT = false;
        }

    }


    public void HideCoinPanel (bool shouldHideImmediate = false)
    {
        if (isShowingCoinJIT)
        {
            if (shouldHideImmediate)
            {
                jitCoinPanelAnimator.SetTrigger("HideImmediate");
            }
            else
            {
                jitCoinPanelAnimator.SetTrigger("Hide");
            }
            isShowingCoinJIT = false;
        }

    }
}
