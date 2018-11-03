using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueCoinController : ContinuePanelController {

    public Text coinCostText;
    private int coinCost;

    public void SetCoinCost(int cost)
    {
        coinCost = cost;
        coinCostText.text = coinCost.ToString();
    }

    public int GetCoinCost()
    {
        return coinCost;
    }


}
