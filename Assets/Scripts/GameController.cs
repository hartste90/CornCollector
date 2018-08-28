using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //magic numbers
	public float delayBeforeEndGameScreenAppears = .7f;
    public int userLevel = 1;
    public float minimumSwipeDistance = 0f;
    public float gameSpeed = 2.5f;


    //links
    public GameObject swipeTooltipObject;
	public TooltipController tooltipController;
	public Transform gameStageParent;
	public UIController uiController;
	public EndgameScreenController endgameScreenController;
	public ContinueScreenController continueScreenController;
    public PlayerController playerController;
    public GameObject playerObject;
    public GameObject playerPrefab;
    public GameObject coinPrefab;
    public GameObject minePrefab;
    public GameObject safePrefab;
    public GameObject bumperPrefab;

    //private links
    private TimeController timeController;

    //tracking
    public float lastTimePlayerWatchedVideo = -3000f;
	public List<GameObject> coinList;
	protected List<GameObject> bumperList;
	protected List<GameObject> mineList;



	public void Awake()
	{
		Tools.screenWidth = Screen.width;
		Tools.screenHeight = Screen.height;
        coinList = new List<GameObject>();
        bumperList = new List<GameObject>();
        mineList = new List<GameObject>();
    }

	public void TestFunc()
	{
		SpawnGameObjectAtPosition (coinPrefab, Vector3.zero);
	}

	void Start()
	{
        timeController = GetComponent<TimeController>();
		endgameScreenController.gameObject.SetActive (false);		
	}

	public void beginTooltip()
	{
		swipeTooltipObject.SetActive (true);
		tooltipController.Show();
		
		beginGameplay ();
	}

	public void beginGameplay()
	{
        //create player
		playerObject = Instantiate (playerPrefab, gameStageParent);
		playerObject.transform.localPosition = Vector3.zero;
		playerController = playerObject.GetComponent<PlayerController>();
        playerController.Init (this);
        playerController.rigidbody.velocity = Vector3.zero;
        //create first safe
        GameObject safeObject = Instantiate(safePrefab, gameStageParent);
        SafeController safeController = safeObject.GetComponent<SafeController>();
        safeController.gameObject.SetActive(true);
        safeController.init(1, 3, 5, this);
    }
	void Update ()
	{
		if(Input.GetKeyDown ("space"))
	        {
			ResetScene();
	        }
		if (Input.GetKeyDown ("u"))
		{
			TestFunc ();
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

	public void PrintScreenDimensionsInWorldSpace()
	{
		Debug.Log (GetRandomLocationOnscreen());

	}

	public static Vector3 GetRandomLocationOnscreen ()
	{

		float horizontalBuffer = 20;
		float verticalBuffer = 20;

		float width = Screen.width;
		float height = Screen.height;

		float halfwidth = width / 2;
		float halfheight = height / 2;

		Vector3 position = new Vector3 (Random.Range (-halfwidth+horizontalBuffer, halfwidth-horizontalBuffer), Random.Range (-halfheight+verticalBuffer, halfheight-verticalBuffer), 0);
		return position;
	}

	public void HandleSafeDestroyed(int numCoins, Transform safeLocation)
	{
		//spawn multiple coins	
		numCoins = 10;	
		for (int i = 0; i < numCoins; i++)
	        {
	                GameObject coin = SpawnGameObjectAtPosition (coinPrefab, safeLocation.localPosition);
			//Debug.Log(safeLocation.localPosition);
	                CoinController coinController = coin.GetComponent<CoinController>();
			//lerp to random position
	                // coinController.LerpToPosition (GetRandomLocationOnscreen (), .5f);
	        }


		//CODE TO CREATE SINGE COIN ON TOP OF SAFE AND CREATE NEW SAFE AT RANDOM SCREEN POSITION
		// GameObject coin = SpawnGameObjectAtPosition (coinPrefab, safeLocation.localPosition);
		CreateSafeForLevel(1);
	}
	public GameObject SpawnGameObjectAtPosition (GameObject gameObject, Vector2 position)
	{
		Vector3 pos3 = new Vector3 (position.x, position.y, 0);
		GameObject obj = Instantiate(gameObject, Vector3.zero, Quaternion.identity, gameStageParent);
		obj.transform.localPosition = pos3;
		RectTransform rect = obj.GetComponent<RectTransform> ();
		rect.anchoredPosition = position;
		if (gameObject.tag != "Safe")
		{
			obj.transform.localScale = new Vector3(3, 3, 1);
		}
		if (gameObject == coinPrefab) 
		{
			obj.transform.localScale = new Vector3 (30,30, 1);
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
			CompleteLevel();
		}

	}

	public void CompleteLevel()
	{
		userLevel++;
//		CreateCoinsForLevel(userLevel);
		// CreateSafeForLevel(userLevel);
	}
	public void CreateCoinsForLevel(int level)
	{
		SpawnMultiple (level, coinPrefab);
	}
	public void CreateSafeForLevel(int userLevel)
	{
	        List<GameObject> safeList = SpawnMultiple (1, safePrefab);
	        int health = 1;
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
		// Debug.Log("Destroying all items onscreen");
	        // Debug.Log(coinList.Count);
		// Debug.Log(bumperList.Count);
		// Debug.Log(mineList.Count);
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
        timeController.handlePlayerDestroyed();
            

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
	        endgameScreenController.populateEndgameScreenContent (uiController.coinCountUILabel.text);
		endgameScreenController.gameObject.SetActive (true);
	}


}
