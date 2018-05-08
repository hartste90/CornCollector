using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScreenController : MonoBehaviour {

	public Text coinCountLabel;
	public Text timerLabel;

	public void populateEndgameScreenContent(string coinCountText, string timerText)
	{
	        coinCountLabel.text = coinCountText;
	        timerLabel.text = timerText;
	}
}
