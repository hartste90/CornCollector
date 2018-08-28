using UnityEngine;
using UnityEngine.UI;

public class EndgameScreenController : MonoBehaviour {

	public Text coinCountLabel;

	public void PopulateEndgameScreenContent(string coinCountText)
	{
	        coinCountLabel.text = coinCountText;
	}
}
