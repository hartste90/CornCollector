using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    public bool dropsMines;
    public Transform playerStartPositionAnchor;

    public RectTransform playerDecal;

    public GameObject playerImage;
    public GameObject explosionPrefab;
    public GameObject playerExplosionPrefab;
    public GameObject minePrefab;
    public GameController gameController;
    public TrailLeaver trailLeaverController;
    public  PointsUpdateTextController pointsUpdateTextController;

    private CharacterController characterController;
    private Vector2 startSwipePosition;
    private Vector3 direction = Vector3.zero;
    private Vector3 lastTouchVector;
    private Rigidbody2D rigidbody;
    private Animator animator;

    private bool isMoving = false;

    private bool isInvincible = false;


    public void Init(GameController controller)
    {
        this.gameController = controller;
        transform.localPosition = playerStartPositionAnchor.localPosition;
    }


    void Awake()
    {
        //dropsMines = true;
        direction = Vector3.zero;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero;
        characterController = GetComponent<CharacterController>();
        lastTouchVector = Vector3.zero;
        animator = GetComponent<Animator>();
        isMoving = false;
    }

    private void Start()
    {
        SetSpriteDirection(new Vector3(1, 0, 0));
    }

    void Update()
    {
        if (GameModel.IsShipInputAllowed())
        {
#if (UNITY_EDITOR || UNITY_STANDALONE)
            DetermineKeyboardDirection();
#else
            DetermineSwipeDirection();
#endif
        }
    }

    public void DetermineKeyboardDirection()
    {
        Vector3 newDirection = Vector3.zero;

        if (Input.GetKeyDown("left"))
        {
            newDirection = Vector3.left;
        }
        else if (Input.GetKeyDown("right"))
        {
            newDirection = Vector3.right;
        }
        else if (Input.GetKeyDown("up"))
        {
            newDirection = Vector3.up;
        }
        else if (Input.GetKeyDown("down"))
        {
            newDirection = Vector3.down;
        }

        if (newDirection != Vector3.zero && newDirection != direction)
        {
            OnChangeDirection(newDirection);
        }

    }



    public void DetermineSwipeDirection()
    {
        Vector3 tempDirection = direction;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startSwipePosition = Input.GetTouch(0).position;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //determine if the finger has moved enough to count as a swipe
            float distance = Vector3.Distance(new Vector3(startSwipePosition.x, startSwipePosition.y, 0), Input.GetTouch(0).position);
            if (distance > 3f)
            {
                //Debug.Log("Detected swipe");
                Vector3 swipeDirection = GetSwipeDirection(Input.GetTouch(0).position);
                if (swipeDirection != Vector3.zero && swipeDirection != direction)
                {
                    OnChangeDirection(swipeDirection);
                }
            }
        }
    }

    public Vector3 GetSwipeDirection(Vector3 currentPosition)
    {
        Vector3 tempDirection = Vector3.zero;
        Vector2 deltaPosition = Input.GetTouch(0).position - startSwipePosition;
        if (deltaPosition.magnitude >= gameController.minimumSwipeDistance)
        {
            if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
            {
                if (deltaPosition.x > 0)
                {
                    tempDirection = Vector3.right;
                }
                else
                {
                    tempDirection = Vector3.left;
                }
            }
            else
            {
                if (deltaPosition.y > 0)
                {
                    tempDirection = Vector3.up;
                }
                else
                {
                    tempDirection = Vector3.down;
                }
            }
        }
        return tempDirection;
    }

    public void OnChangeDirection(Vector3 tempDirection)
    {
        if (isMoving == false)
        {
            //if not on center screen (could be interacting with UI), don't do anything
            if (!SwipeStartsOnCenterScreen())
            {
                return;
            }
            gameController.OnPlayerBeginsMovement();
            isMoving = true;
        }

        if (dropsMines)
        {
            gameController.SpawnGameObjectAtPosition(minePrefab, transform.GetComponent<RectTransform>().anchoredPosition);
        }
        SetDirection(tempDirection);
    }

    private bool SwipeStartsOnCenterScreen()
    {
#if (!(UNITY_EDITOR || UNITY_STANDALONE)) //if mobile
        if (startSwipePosition.y <= Screen.height * .17)
            return false;
#endif
        return true;
    }

    protected void SetDirection (Vector3 tempDirection )
	{
		direction = tempDirection;
		rigidbody.velocity = direction * gameController.gameSpeed;
        SetSpriteDirection(tempDirection);

	}


    private void SetSpriteDirection(Vector3 direction)
    {
        if (direction == new Vector3(1, 0, 0))
        {
            playerDecal.localEulerAngles = new Vector3(0, 0, -90);
        }
        else if (direction == new Vector3(-1, 0, 0))
        {
            playerDecal.localEulerAngles = new Vector3(0, 0, 90);
        }
        else if (direction == new Vector3(0, 1, 0))
        {
            playerDecal.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            playerDecal.localEulerAngles = new Vector3(0, 0, 180);

        }
    }

	public void OnHitMine()
	{
		GameObject playerExplosion1 = Instantiate (playerExplosionPrefab, transform.parent, false);
		GameObject playerExplosion2 = Instantiate (playerExplosionPrefab, transform.parent, false);
        playerExplosion1.GetComponent<ExplosionController>().gameController = gameController;
        playerExplosion2.GetComponent<ExplosionController>().gameController = gameController;

        playerExplosion1.transform.localPosition = transform.localPosition;
		playerExplosion2.transform.localPosition = transform.localPosition;
		playerExplosion2.transform.Rotate (0,0,45);
        //CreatePhysicalExplosion ();

        DestroySelf();
        gameController.HandlePlayerDestroyed();
	}

    public void DestroySelf()
    {
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

    public void SetInvincible(bool setInvincible)
    {
        this.isInvincible = setInvincible;
        if (this.isInvincible)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.isInvincible){
            return;
        }
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

    public void AnimateIntro()
    {
        animator.SetTrigger("Intro");
    }

    public void OnIntroAnimationComplete()
    {
        GameModel.EnableShipInput();
        GetComponent<WrapAroundBehavior>().CreateGhostShips();
        BeginDropExhaust();
    }

    public void BeginDropExhaust()
    {
        trailLeaverController.isDroppingExhaust = true;
    }

    public void ShowCoinCollectUpdate(int amount)
    {
        pointsUpdateTextController.Show(amount);
    }
}
