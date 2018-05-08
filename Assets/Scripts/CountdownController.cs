using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour {

	public GameController gameController;

	public void handleCountdownAnimationComplete()
	{
	        gameController.beginTooltip ();
	}


}
