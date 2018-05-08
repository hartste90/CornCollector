using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public float delayBeforeEndGameScreenAppears;
	public int userLevel;

	public GameObject swipeTooltipObject;
	public SafeController initialSafeController;
	public TooltipController tooltipController;
	public Transform gameStageParent;
	public UIController uiController;
	public EndgameScreenController endgameScreenController;
	public ContinueScreenController continueScreenController;
	public float lastTimePlayerWatchedVideo;

	public int numStartingMines;
	public int numStartingBumpers; 
	public int numStartingCoins; 

	public float minimumSwipeDistance;

	public PlayerController playerController;
	public GameObject playerObject;

	public float gameSpeed;
	public GameObject playerPrefab;
	public GameObject coinPrefab;
	public GameObject minePrefab;
	public GameObject safePrefab;
	public GameObject bumperPrefab;

	public List<GameObject> coinList;
	protected List<GameObject> bumperList;
	protected List<GameObject> mineList;

	public void Awake()
	{
		Tools.screenWidth = Screen.width;
		Tools.screenHeight = Screen.height;
	}


	void Start()
	{

		endgameScreenController.gameObject.SetActive (false);
		userLevel = 1;
		coinList = new List<GameObject> ();
		bumperList = new List<GameObject> ();
		mineList = new List<GameObject> ();
		lastTimePlayerWatchedVideo = -3000f;

	}

	public void beginTooltip()
	{
		swipeTooltipObject.SetActive (true);
		tooltipController.Show();
		initialSafeController.gameObject.SetActive (true);
		initialSafeController.init(1,3,5, this);
		beginGameplay ();
	}

	public void beginGameplay()
	{
		//spawn starting coin
//		SpawnMultiple(numStartingCoins, coinPrefab);
		//spawn starting safes
//		List<GameObject> safeList = SpawnMultiple(1, safePrefab);
//		safeList[0].GetComponent<SafeController>().init (3, 3, 5, this);
		//spawn starting mines
		SpawnMultiple(numStartingMines, minePrefab);
		//spawn starting bumpers
		SpawnMultiple(numStartingBumpers, bumperPrefab);
		playerObject = Instantiate (playerPrefab, gameStageParent);
		playerObject.transform.localScale = new Vector3(384, 384, 1);
		playerObject.transform.localPosition = Vector3.zero;
	        playerController = playerObject.GetComponent<PlayerController>();
	        playerController.Init (this);
	        playerController.rigidbody.velocity = Vector3.zero;	
		playerObject.transform.position = Vector3.zero;
	}
	void Update ()
	{
		if(Input.GetKeyDown ("space"))
	        {
			ResetScene();
	        }
	}

	public void ResetScene()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
//	        uiController.ResetUI();
//		DestroyAllItemsOnscreen();
//	        Start();
	}

	public List<GameObject> SpawnMultiple (int numToSpawn, GameObject gameObject)
	{
	        List<GameObject> objectList = new List<GameObject>();
		for (int i = 0; i < numToSpawn; i++) 
		{
			GameObject obj = SpawnGameObject (gameObject);
			objectList.Add(obj);
		}
		return objectList;
	}



	public GameObject SpawnGameObject (GameObject gameObject)
	{
		Vector3 screenPosition = GetRandomLocationOnscreen ();
		return SpawnGameObjectAtPosition (gameObject, screenPosition);
	}

	public static Vector3 GetRandomLocationOnscreen ()
	{
		Vector2 topRightCorner = new Vector2 (1, 1);
		Vector2 edgeVector = Camera.main.ViewportToWorldPoint (topRightCorner);
//		Debug.Log("EDge Vector: " + edgeVector.ToString ());
//		Debug.Log("w/h: " + Screen.currentResolution.width + "," + Screen.currentResolution.height);

		float halfheight = edgeVector.y;
		float halfwidth = edgeVector.x;
		Vector3 position = new Vector3 (Random.Range (-halfwidth, halfwidth), Random.Range (-halfheight, halfheight), 0);
//		Debug.Log("Random Position: " + position.ToString ());
		return position;

	}

	public void HandleSafeDestroyed(int numCoins, Transform safeLocation)
	{
		for (int i = 0; i < numCoins; i++)
	        {
	                GameObject coin = SpawnGameObjectAtPosition (coinPrefab, safeLocation.position);
	                CoinController coinController = coin.GetComponent<CoinController>();
	                coinController.LerpToPosition (GetRandomLocationOnscreen (), .5f);
	        }
	}
	public GameObject SpawnGameObjectAtPosition (GameObject gameObject, Vector3 position)
	{
		GameObject obj = Instantiate(gameObject, position, Quaternion.identity, gameStageParent);
		if (gameObject.tag != "Safe")
		{
			obj.transform.localScale = new Vector3(384, 384, 1);
		}
		if (gameObject == coinPrefab) 
		{
			obj.transform.localScale = new Vector3 (384, 384, 1);
			coinList.Add (obj);
		}
		else if (gameObject == minePrefab)
		{
		        mineList.Add (obj);
		}
		else if (gameObject == bumperPrefab)
		{
		        bumperList.Add (obj);
		}
		return obj;
	}

	public void CheckCoinsCollected(GameObject coin)
	{
	        uiController.AddCoinsCollected(1);
	        coinList.Remove (coin);
	        Destroy(coin);
		if (coinList.Count == 0) 
		{
//			Debug.Log ("Victory: collected final coin");
			CompleteLevel();
		}

	}

	public void CompleteLevel()
	{
		userLevel++;
//		CreateCoinsForLevel(userLevel);
		CreateSafeForLevel(userLevel);
	}
	public void CreateCoinsForLevel(int level)
	{
		SpawnMultiple (level, coinPrefab);
	}
	public void CreateSafeForLevel(int userLevel)
	{
	        List<GameObject> safeList = SpawnMultiple (1, safePrefab);
	        int health = 3;
		int coinValue =  Random.Range (1, userLevel)+1;
		int keyCost = Random.Range (coinValue, coinValue + 3);
	        safeList[0].GetComponent<SafeController>().init (health, coinValue, keyCost, this);
	}

	public void CreateSafeForLevel()
	{
		SpawnMultiple (1, safePrefab);
	} 

	public void DestroyAllItemsOnscreen()
	{
	        Debug.Log(coinList.Count);
		Debug.Log(bumperList.Count);
		Debug.Log(mineList.Count);
	        for (int i = coinList.Count-1; i >= 0; i-- )
	        {
			Destroy (coinList[i].gameObject);
	        }
		for (int i = bumperList.Count-1; i >= 0; i-- )
	        {
			Destroy (bumperList[i].gameObject);
	        }
		for (int i = mineList.Count-1; i >= 0; i-- )
	        {
			Destroy (mineList[i].gameObject);
	        }
		Destroy (playerObject);
	}

	public void handlePlayerDestroyed()
	{
	        uiController.PauseTimer ();

	        PlayerPrefs.SetInt ("lastScore", uiController.coinCountNum);
		if (PlayerPrefs.GetInt ("bestScore") < uiController.coinCountNum)
	        {
			PlayerPrefs.SetInt ("bestScore", uiController.coinCountNum);
	        }
		StartCoroutine (ShowEndgameScreenAfterSeconds (delayBeforeEndGameScreenAppears));
	}

	IEnumerator ShowEndgameScreenAfterSeconds (float waitTime) 
	{
	        yield return new WaitForSeconds(waitTime);
	        endgameScreenController.populateEndgameScreenContent (uiController.coinCountUILabel.text, uiController.timerUILabel.text);
		endgameScreenController.gameObject.SetActive (true);
	}


}
