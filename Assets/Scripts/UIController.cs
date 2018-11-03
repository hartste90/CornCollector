using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject uiView;

    //public Image background;
    public SpriteRenderer coin;
	public Text coinCountUILabel;

    public GameObject titleUI;

	// Use this for initialization
	void Start () 
    {
        ResetUI();
	}

	public void ResetUI()
	{
        HideGameUI();
        SetCoinText(0);
    }

	public void SetCoinText (int numCoins)
	{
        coinCountUILabel.text = numCoins + "";
	}

    public void ShowGameUI()
    {
        coin.enabled = true;
        coinCountUILabel.enabled = true;
    }
    public void HideGameUI()
    {
        coin.enabled = false;
        coinCountUILabel.enabled = false;
    }

    public void ShowTitleUI()
    {
        titleUI.SetActive(true);
    }

    public void HideTitleUI()
    {
        titleUI.SetActive(false);
    }
}
