using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
   
    public bool dropsMines;

    public Transform playerDecal;

    public GameObject explosionPrefab;
	public GameObject playerExplosionPrefab;
	public GameObject minePrefab;
    public GameController gameController;

    private CharacterController characterController;
    private Vector2 startSwipePosition;
    private Vector3 direction = Vector3.zero;
    private Vector3 lastTouchVector;
    private Rigidbody2D rigidbody;
    private Animator animator;

	public void Init(GameController controller)
	{
        this.gameController = controller;
        transform.localPosition = Vector3.zero;
    }

    void Start () 
    {
        //dropsMines = true;
        direction = Vector3.zero;
        rigidbody = GetComponent <Rigidbody2D>();
        rigidbody.velocity =Vector3.zero;
        characterController = GetComponent <CharacterController>();
        lastTouchVector = Vector3.zero;
        animator = GetComponent<Animator>();
	}

    void Update()
    {

        //hide the tooltip if we are moving and it hasn't been hidden before
        if (gameController.swipeTooltipObject.activeSelf && direction != Vector3.zero)
        {
            gameController.HideTooltip();
        }

        //Different inputs for editor or live game
#if (UNITY_EDITOR || UNITY_STANDALONE)
        DetermineKeyboardDirection();
#else
    	DetermineSwipeDirection();
#endif
    }

    public void DetermineKeyboardDirection()
	{
        Vector3 newDirection = Vector3.zero;

		if (Input.GetKeyDown ("left"))
		{
            //Debug.Log("left");
            playerDecal.eulerAngles = new Vector3 (0, 0, 90);
            newDirection = Vector3.left;
        }
		else if (Input.GetKeyDown ("right"))
		{
            //Debug.Log("right");
            playerDecal.eulerAngles = new Vector3(0, 0, -90);
            newDirection = Vector3.right;
        }
		else if (Input.GetKeyDown ("up"))
		{
            //Debug.Log("up");
            playerDecal.eulerAngles = new Vector3(0, 0, 0);
            newDirection = Vector3.up;
        }
		else if (Input.GetKeyDown ("down"))
		{
            //Debug.Log("down");
            playerDecal.eulerAngles = new Vector3(0, 0, 180);
            newDirection = Vector3.down;
		}

        if (newDirection != Vector3.zero && newDirection != direction)
		{
            OnChangeDirection(newDirection);
		}
		
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
                Vector3 swipeDirection = GetSwipeDirection(Input.GetTouch(0).position);
                if (swipeDirection != Vector3.zero && swipeDirection != direction)
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
                    Debug.Log("Swiping: RIGHT");
					tempDirection = Vector3.right;
				} else {
					Debug.Log ("Swiping: LEFT");
					tempDirection = Vector3.left;
				}
			} 
            else {
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

	public void OnChangeDirection( Vector3 tempDirection)
	{
		if(dropsMines)
		{
			gameController.SpawnGameObjectAtPosition (minePrefab, transform.GetComponent<RectTransform>().anchoredPosition);
		}
        SetDirection(tempDirection);
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
		//CreatePhysicalExplosion ();

		gameController.HandlePlayerDestroyed();
        GetComponent<WrapAroundBehavior>().DestroyAllGhosts();
        Destroy(gameObject);
	}

	//public void CreatePhysicalExplosion()
	//{
	//	Vector3 explosionPos = transform.position;
	//	Collider[] colliders = Physics.OverlapSphere(explosionPos, 10000f);
    //  Debug.Log(colliders.Length);
	//	foreach (Collider hit in colliders)
	//	{
	//		Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

	//		if (rb != null)
	//			AddExplosionForce2D( rb, 10f, explosionPos, 100f);
	//	}
	//}

    //public void AddExplosionForce2D(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    //{
    //    var dir = (body.transform.position - explosionPosition);
    //    float wearoff = 1 - (dir.magnitude / explosionRadius);
    //    body.AddForce(dir.normalized * explosionForce * wearoff);
    //}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Mine":
                OnHitMine();
                collision.gameObject.GetComponent<MineController>().MineExplode();
                break;
            case "Explosion":
                OnHitMine();
                break;
            case "Safe":
                OnHitMine();
                break;
        }
    }
}
