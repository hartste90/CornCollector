using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public bool isTimerCounting;
	public Text coinCountUILabel;
	public int coinCountNum;

	public Text timerUILabel;
	public float timerSecondsPlayed;

	// Use this for initialization
	void Start () {
	        isTimerCounting = true;
	        coinCountNum = 0;
		SetCoinText (0);
		timerSecondsPlayed = 0;
	}

	public void ResetUI()
	{
	        Start();
	}

	void Update ()
	{
		if (isTimerCounting) 
		{
			timerSecondsPlayed += Time.deltaTime;
			SetTimerText (timerSecondsPlayed);
		}
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

	public void SetTimerText (float seconds)
	{
	        timerUILabel.text = Mathf.Floor(seconds) + "";
	}

	public void PauseTimer()
	{
	        isTimerCounting = false;
	}


}
