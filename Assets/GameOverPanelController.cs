

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour {


    public RollupController rollupController;
    public OptionsPanelController optionsPanelController;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void EnablePanelInput()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisablePanelInput()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

    }

    //just shows the pink coin, without the options panel
    public void ShowWithPurchase()
    {
        rollupController.HidePanelsForPurchase();
        optionsPanelController.HidePanelsForPurchase();
        GetComponent<Animator>().SetTrigger("Show");
    }

    public void ShowAfterPurchase()
    {
        //Debug.Break();
        rollupController.ShowPanelsForPurchase();
        optionsPanelController.ShowPanelsForPurchase();
        GetComponent<Animator>().SetTrigger("Show");

    }
}
