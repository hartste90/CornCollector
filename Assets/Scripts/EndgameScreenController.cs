using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameScreenController : MonoBehaviour {

	public Text coinCountLabel;

	public void populateEndgameScreenContent(string coinCountText)
	{
	        coinCountLabel.text = coinCountText;
	}
}
