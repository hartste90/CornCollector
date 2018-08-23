using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAroundScreen : MonoBehaviour {

	float halfWidth;
	float halfHeight;

	// Use this for initialization
	void Start () {
		halfWidth = Screen.width/2;
		halfHeight = Screen.height/2;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localPosition.x > halfWidth)
		{
			BlinkToLeftSide();
		}
		else if (transform.localPosition.x < -halfWidth)
		{
			BlinkToRightSide();
		}
		else if (transform.localPosition.y > halfHeight)
		{
			BlinkToBottomSide();
		}
		else if (transform.localPosition.x < - halfHeight)
		{
			BlinkToTopSide();
		}
	}


	private void BlinkToLeftSide()
	{
		Vector3 newPos = new Vector3( -halfWidth, transform.localPosition.y, 0);
		transform.localPosition = newPos;
	}

	private void BlinkToRightSide()
	{
		Vector3 newPos = new Vector3( halfWidth, transform.localPosition.y, 0);
		transform.localPosition = newPos;
	}

	private void BlinkToTopSide()
	{
		Vector3 newPos = new Vector3( transform.localPosition.x, halfHeight, 0);
		transform.localPosition = newPos;
	}

	private void BlinkToBottomSide()
	{
		Vector3 newPos = new Vector3( transform.localPosition.x, -halfHeight, 0);
		transform.localPosition = newPos;
	}
}
