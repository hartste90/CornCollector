using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonController : MonoBehaviour {

    public OptionsPanelController optionsController;

    public int continueCoinCost;
    public Text costText;

    public void SetCoinCost(int cost)
    {
        this.continueCoinCost = cost;
        this.costText.text = this.continueCoinCost.ToString();
    }

    public void HandleContinueCoinButtonPressed()
    {
        //if have enough coins already, take away coins and continue game
        if (PlayerPrefManager.GetPinkCount() >= this.continueCoinCost)
        {
            PlayerPrefManager.SubtractPinkCoins(this.continueCoinCost);
            Debug.Log("New pink coin count: " + PlayerPrefManager.GetPinkCount());
            GameModel.numAttempts++;
            optionsController.OnContinueGame();
        }
        //otherwise show the shop screen to buy those coins
        else
        {
            optionsController.OnShowStoreFromEndgame();
        }
    }
}
