using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text coinCountUILabel;
	public int coinCountNum;

	// Use this for initialization
	void Start () 
    {
        ResetUI();
	}

	public void ResetUI()
	{
        coinCountNum = 0;
        SetCoinText(0);
    }
	
	public void AddCoinsCollected (int numCoins)
	{
        coinCountNum += numCoins;
        SetCoinText (coinCountNum);
	}

	public void SetCoinText (int numCoins)
	{
        coinCountUILabel.text = numCoins + "";
	}
}
