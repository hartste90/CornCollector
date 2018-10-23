using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject uiView;

    //public Image background;
    public SpriteRenderer coin;
	public Text coinCountUILabel;

	// Use this for initialization
	void Start () 
    {
        ResetUI();
	}

	public void ResetUI()
	{
        HideUI();
        SetCoinText(0);
    }

	public void SetCoinText (int numCoins)
	{
        coinCountUILabel.text = numCoins + "";
	}

    public void ShowUI()
    {
        //background.enabled = true;
        coin.enabled = true;
        coinCountUILabel.enabled = true;
    }
    public void HideUI()
    {
        //background.enabled = false;
        coin.enabled = false;
        coinCountUILabel.enabled = false;
    }
}
