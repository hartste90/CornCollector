using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

	public GameController gameController;
	public Vector3 direction = Vector3.zero;
	public GameObject explosionPrefab;
	public GameObject playerExplosionPrefab;
	public GameObject minePrefab;
	public Rigidbody2D rigidbody;
	protected CharacterController characterController;
	protected Vector2 startSwipePosition;

	protected Vector3 lastTouchVector;

	public bool dropsMines;
	public Animator animator;


	void OnDrawGizmos() 
	{
	        if (lastTouchVector != Vector3.zero)
	        {
	                Gizmos.DrawLine (transform.position, lastTouchVector);
	        }
     }

	public void Init(GameController controller)
	{
	        this.gameController = controller;
	}

	// Use this for initialization
	void Start () {
	        direction = Vector3.zero;
	        rigidbody = GetComponent <Rigidbody2D>();
	        rigidbody.velocity =Vector3.zero;
	        characterController = GetComponent <CharacterController>();
	        lastTouchVector = Vector3.zero;
	        dropsMines = true;
	        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
//		MoveInCurrentDirection();
		if (direction == Vector3.zero)
		{
		        rigidbody.velocity = direction;
		}
		if (direction != Vector3.zero && gameController.swipeTooltipObject.activeSelf)
		{
			gameController.tooltipController.Hide();
		}

#if UNITY_EDITOR
		DetermineTapDirection();
#else
		DetermineSwipeDirection();
#endif
	}

	public void DetermineTapDirection()
	{
		Vector3 tempDirection = direction;
		Vector3 touchPosition = Vector3.zero;
		//mimic touch

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			Vector2 touchPos = Input.GetTouch(0).position;
			Vector3 worldpos = Camera.main.ScreenToWorldPoint(touchPos); 
			touchPosition = new Vector3 (worldpos.x, worldpos.y, 0);
		} 

		if (Input.GetKeyDown ("left"))
		{
		        touchPosition = new Vector3 (-1000, 0, 0);
		}
		else if (Input.GetKeyDown ("right"))
		{
			touchPosition = new Vector3 (1000, 0, 0);
		}
		else if (Input.GetKeyDown ("up"))
		{
			touchPosition = new Vector3 (0, 1000, 0);
		}
		else if (Input.GetKeyDown ("down"))
		{
			touchPosition = new Vector3 (0, -1000, 0);
		}

		if (touchPosition != Vector3.zero)
		{
			Vector3 difference =  touchPosition - transform.position;
//		        difference.Normalize ();
		        lastTouchVector = touchPosition;
//		        Debug.Log ("Difference: " + difference);
			float xDiffMag = Mathf.Abs (touchPosition.x - transform.position.x);
			float yDiffMag = Mathf.Abs (touchPosition.y - transform.position.y);
			Vector3 deltaPosition = new Vector3 (touchPosition.x - transform.position.x, touchPosition.y - transform.position.y, transform.position.z);
//			Debug.Log("Delta Pos: "  + deltaPosition+ " XDiff: " + xDiffMag + " YDiff: " + yDiffMag);
			//HORIZONTAL CHANGE
			if (Mathf.Abs (difference.x) >= Mathf.Abs (difference.y)) 
			{
				if (difference.x > 0) 
				{
//					Debug.Log ("Tapping: RIGHT");
					animator.SetTrigger ("PlayerRight");

					tempDirection = Vector3.right;
				}
				else
				{
//					Debug.Log ("Tapping: LEFT");
					animator.SetTrigger ("PlayerLeft");

					tempDirection = Vector3.left;
				}
			}
			//VERTICAL CHANGE
			else
			{
				if (difference.y > 0) 
				{
//					Debug.Log ("Tapping: UP");
					animator.SetTrigger ("PlayerUp");

					tempDirection = Vector3.up;
				}
				else
				{
//					Debug.Log ("Tapping: DOWN");
					animator.SetTrigger ("PlayerDown");

					tempDirection = Vector3.down;
				}
			}
				
			if(tempDirection != direction)
			{
				OnChangeDirection(tempDirection);
			}
		}


//
		
	}



	public void DetermineSwipeDirection ()
	{
		Vector3 tempDirection = direction;
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) 
		{
			startSwipePosition = Input.GetTouch (0).position;
		} 
		else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved)
		{
		        //determine if the finger has moved enough to count as a swipe
		        float distance = Vector3.Distance (new Vector3(startSwipePosition.x, startSwipePosition.y, 0), Input.GetTouch (0).position );
		        if (distance > 3f)
		        {
		                Debug.Log("Detected swipe");
				Vector3 swipeDirection =  GetSwipeDirection(Input.GetTouch(0).position);
				if(swipeDirection != Vector3.zero && swipeDirection != direction)
			        {
					OnChangeDirection(swipeDirection);
			        }
		        }

		} 
	}

	public Vector3 GetSwipeDirection (Vector3 currentPosition)
	{
		Vector3 tempDirection = Vector3.zero;
		Vector2 deltaPosition = Input.GetTouch (0).position - startSwipePosition;
		if (deltaPosition.magnitude >= gameController.minimumSwipeDistance) {
			if (Mathf.Abs (deltaPosition.x) > Mathf.Abs (deltaPosition.y)) {
				if (deltaPosition.x > 0) {
					Debug.Log ("Swiping: RIGHT");
					tempDirection = Vector3.right;
				} else {
					Debug.Log ("Swiping: LEFT");

					tempDirection = Vector3.left;
				}
			} else {
				if (deltaPosition.y > 0) {
					Debug.Log ("Swiping: UP");

					tempDirection = Vector3.up;
				} else {
					Debug.Log ("Swiping: DOWN");

					tempDirection = Vector3.down;
				}
			}
		}
		return tempDirection;
	}

	public void MoveInCurrentDirection()
	{
	        characterController. Move (direction * gameController.gameSpeed);

	}

	public void DetermineDirectionChange()
	{
//	Debug.Log (Input.GetAxis ("Vertical"));
//		Debug.Log (Input.GetAxis ("Horizontal"));
//
		Vector3 tempDirection = direction;
	        if(Input.GetKey ("left"))
	        {
	                animator.SetTrigger ("PlayerLeft");
			tempDirection = Vector3.left;
	        }
		else if(Input.GetKey ("right"))
	        {
			animator.SetTrigger ("PlayerRight");
			tempDirection = Vector3.right;
	        }
		else if(Input.GetKey ("up"))
	        {
			animator.SetTrigger ("PlayerUp");
			tempDirection = Vector3.up;
	        }
		else if (Input.GetKey ("down"))
		{	                
			animator.SetTrigger ("PlayerDown");
			tempDirection = Vector3.down;
	        }

	        if(tempDirection != direction)
	        {
	                OnChangeDirection(tempDirection);
	        }
	}

	public void OnChangeDirection( Vector3 tempDirection)
	{
		gameController.SpawnGameObjectAtPosition (minePrefab, transform.position);
		SetDirection (tempDirection);

	}
	protected void SetDirection (Vector3 tempDirection )
	{
		direction = tempDirection;
		rigidbody.velocity = direction * gameController.gameSpeed;
	}

	public void OnHitMine()
	{
		GameObject playerExplosion1 = Instantiate (playerExplosionPrefab, transform.parent);
		GameObject playerExplosion2 = Instantiate (playerExplosionPrefab, transform.parent);

		playerExplosion1.transform.localPosition = transform.localPosition;
		playerExplosion2.transform.localPosition = transform.localPosition;
		playerExplosion2.transform.Rotate (0,0,45);

//		CreatePhysicalExplosion ();

		gameController.handlePlayerDestroyed();
		Time.timeScale = 0.2f;
		Destroy(gameObject);


	}



	public void CreatePhysicalExplosion()
	{
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, 100f);
		foreach (Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null)
				rb.AddExplosionForce(10f, explosionPos, 100f, 3.0F);
		}
	}
		


	public void OnHitBumper()
	{
	    Vector3 oppositeDirection = GetOppositeDirection(direction);
		SetDirection (oppositeDirection);
	}

	public void OnHitCoin(GameObject coin)
	{
		gameController.CheckCoinsCollected (coin);
	}

	public void OnTriggerEnter2D(Collider2D collider) 
	{
		if (collider.gameObject.tag == "Coin")
                {
                        OnHitCoin(collider.gameObject);
                }
        }
	public void OnCollisionEnter2D(Collision2D collision)
         {
//		Debug.Log ("COllision: " + collision.gameObject.tag);
            if (collision.gameObject.tag == "Mine")
            {
					OnHitMine ();
					collision.gameObject.GetComponent<MineController>().DestroySelf ();
            }
            else if (collision.gameObject.tag == "Explosion")
            {
			OnHitMine ();
            }
            else if (collision.gameObject.tag == "Safe")
            {
                OnHitMine();
            }
            else if (collision.gameObject.tag == "Bumper")
            {
                    OnHitBumper();
            }
               
         }

	protected Vector3 GetOppositeDirection(Vector3 direction)
	{
	        if(direction == Vector3.forward)
	                return Vector3.back;
	       else if (direction == Vector3.back)
	                return Vector3.forward;
		else if (direction == Vector3.left)
	                return Vector3.right;
		else
	                return Vector3.left;

	}
}
