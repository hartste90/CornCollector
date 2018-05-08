using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueScreenController : MonoBehaviour {

	public GameController gameController;

	public GameObject continueByCoinButton;
	public GameObject continueByGemButton;
	public GameObject continueByVideoButton;

	public Text coinCostText;
	public Text videoFreeText;
	public Text videoTimerText;

	public void init (GameController gameController)
	{
	        this.gameController = gameController;
	}
	public void Show()
	{
	        PopulateScreenFromData();
	        gameObject.SetActive (true);
	}
	public void Hide()
	{
	        gameObject.SetActive (false);
	}
	public void PopulateScreenFromData()
	{
	        //gem button
	        //if they have enough gems, show USE GEMS and setup button callback
	        //else show Buy GEMS AND CONTINUE BUTTON
	        if (gameController.uiController.coinCountNum < 50)
	        {
	                continueByCoinButton.GetComponent<Button>().interactable = false;
	        }
	        if (Time.time - gameController.lastTimePlayerWatchedVideo > 300)
	        {
	                videoFreeText.gameObject.SetActive (true);
	                videoTimerText.gameObject.SetActive (false);
	                //TODO: setup callback for button
	        }
	        else
	        {
			continueByVideoButton.GetComponent<Button>().interactable = false;
			videoFreeText.gameObject.SetActive (false);
	                videoTimerText.gameObject.SetActive (true);
			videoTimerText.text = Mathf.RoundToInt(Time.time - gameController.lastTimePlayerWatchedVideo).ToString ();
			//TODO: setup callback for button
	        }
	}
}
