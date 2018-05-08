using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	public bool isLerping;
	public float timeSinceStartedLerp;
	public float timeToLerp;
	public Vector3 lerpStartPosition;
	public Vector3 lerpTargetPosition;

	void Start()
	{
	}
	void OnCollisionEnter2D(Collision2D collider) 
	{
//	        Debug.Log("Coin ENTER: " + collider.gameObject.tag);
		if (collider.gameObject.tag == "Player") {
			return;
		} 
		else 
		{
			MoveToUnoccupiedSpace ();
		}
	}

	void OnTriggerStay2D(Collider2D collider) 
	{
//	        Debug.Log("Coin STAY: " + collider.gameObject.tag);
		if (collider.gameObject.tag == "Player") {
			return;
		} 
		else 
		{
			MoveToUnoccupiedSpace ();
		}
	}
	public void MoveToUnoccupiedSpace()
	{
		Debug.Log ("Moved to new location");
		transform.position = GameController.GetRandomLocationOnscreen ();
	}

//	void FixedUpdate()
//	{
//	        if (transform.localPosition.x <= (-Screen.width/2 - Screen.width *.1)
//			|| transform.localPosition.x >= (Screen.width/2 + Screen.width *.1)
//			|| transform.localPosition.y <= (-Screen.height/2 - Screen.height *.1)
//			|| transform.localPosition.y >= (Screen.height/2 + Screen.height *.1))
//	        {
//	                Debug.Log ("Turning around: " + transform.position.ToString ());
//			GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * -1;
//	        }
//	}

	void Update()
	{
	        if (isLerping == true)
	        {
			timeSinceStartedLerp += Time.deltaTime;
	                transform.position = Vector3.Lerp(lerpStartPosition, lerpTargetPosition, timeSinceStartedLerp/timeToLerp );
	        }

	}

	public void LerpToPosition(Vector3 targetPosition, float timeToLerp)
	{
	        isLerping = true;
	        timeSinceStartedLerp = 0f;
		lerpStartPosition = transform.position;
	        lerpTargetPosition = targetPosition;
	        this.timeToLerp = timeToLerp;

	        
	        
	}



}
