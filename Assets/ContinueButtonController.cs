using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonController : MonoBehaviour {

    public OptionsPanelController optionsController;
    public ContinueAdController continueAdController;
    public ContinueFreeController continueFreeController;
    public ContinueCoinController continueCoinController;
    public StoreButtonController storeButtonController;

    private Animator animator;
    private Button button;

    public delegate void ContinueButtonPressCallback();
    public ContinueButtonPressCallback continueButtonCallback;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    public void Show()
    {
        SetInteractable(true);
        animator.SetTrigger("Show");
    }
    public void ShowImmediate()
    {
        SetInteractable(true);
        animator.SetTrigger("ShowImmediate");
    }
    public void Hide()
    {
        SetInteractable(false);
        animator.SetTrigger("Hide");
    }
    public void HideImmediate()
    {
        this.animator.SetTrigger("HideImmediate");
    }

    public void ShowSmall(bool shouldShowImmediately)
    {
        SetInteractable(true);
        if (shouldShowImmediately)
        {
            this.animator.SetTrigger("ShowSmallImmediate");
        }
        else
        {
            this.animator.SetTrigger("ShowSmall");
        }
    }

    public void SetInteractable(bool setInteractive)
    {
        this.button.interactable = setInteractive;
    }

    public void SetContinueButtonCallback(ContinueButtonPressCallback callback)
    {
        continueButtonCallback = callback;
    }

    public void HandleButtonPressed()
    {
        continueButtonCallback();
    }

    public void ShowAsAd()
    {
        continueAdController.Show();
        continueCoinController.Hide();
        continueButtonCallback = HandleContinueAd;
    }

    public void ShowAsFree()
    {
        continueAdController.Hide();
        continueCoinController.Hide();
        continueFreeController.Show();
        continueButtonCallback = HandleContinueFree;
    }

    public void ShowAsCoin()
    {
        continueAdController.Hide();
        continueCoinController.Show();
        continueButtonCallback = HandleContinueCoinButtonPressed;
    }

    public void HandleContinueAd()
    {
        //STUB
        optionsController.HandleAdButtonPressed();
        Debug.Log("Continueing as ad");
    }

    public void HandleContinueFree()
    {
        GameModel.numAttempts++;
        optionsController.OnContinueGame();
    }

    public void HandleContinueCoinButtonPressed()
    {
        //if have enough coins already, take away coins and continue game
        if (PlayerPrefManager.GetPinkCount() >= continueCoinController.GetCoinCost())
        {
            PlayerPrefManager.SubtractPinkCoins( continueCoinController.GetCoinCost());
            GameModel.numAttempts++;
            optionsController.OnContinueGame();
        }
        //otherwise show the shop screen to buy those coins
        else
        {
            optionsController.ShowStoreJIT();
        }
    }

    public void SetCoinCost(int cost)
    {
        continueCoinController.SetCoinCost(cost);
    }


}
