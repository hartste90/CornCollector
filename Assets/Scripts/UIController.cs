using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public GameObject uiView;
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
        uiView.SetActive(true);
    }
    public void HideUI()
    {
        uiView.SetActive(false);
    }
}
