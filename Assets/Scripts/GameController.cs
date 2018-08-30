using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //magic numbers
	public float delayBeforeEndGameScreenAppears = .7f;
    public int userLevel = 1;
    public int numSafes = 1;
    public float minimumSwipeDistance = 0f;
    public float gameSpeed = 2.5f;
    public int numCoinsInSafe = 10;


    //links
    public GameObject swipeTooltipObject;
    public GameObject playerPrefab;
    public GameObject coinPrefab;
    public GameObject minePrefab;
    public GameObject safePrefab;
    public Transform gameStageParent;
	public UIController uiController;
	public EndgameScreenController endgameScreenController;
	public ContinueScreenController continueScreenController;

    //private links
    private TimeController timeController;
    private TooltipController tooltipController;
    private PlayerController playerController;
    private GameObject playerObject;

    //tracking
    public float lastTimePlayerWatchedVideo = -3000f;
	public List<GameObject> coinList;
	public List<GameObject> mineList;

    //void OnGUI()
    //{
    //    //Delete all of the PlayerPrefs settings by pressing this Button
    //    if (GUI.Button(new Rect(100, 200, 200, 60), "Delete"))
    //    {
    //        PlayerPrefs.DeleteAll();
    //    }
    //}


    public void Awake()
	{
        //record device dimensions
		Tools.screenWidth = Screen.width;
		Tools.screenHeight = Screen.height;
        //instantiate lists
        coinList = new List<GameObject>();
        mineList = new List<GameObject>();
    }

    // a test function to trigger custom functionality for debugging
	public void TestFunc()
	{
		SpawnGameObjectAtRandomPosition (safePrefab);
	}

	void Start()
	{
        //setup private links
        tooltipController = swipeTooltipObject.GetComponent<TooltipController>();
        timeController = GetComponent<TimeController>();
        //hide the end game screen if it's been shown
		endgameScreenController.gameObject.SetActive (false);		
	}

    //shows the tooltip at the beginning of the game
	public void HandleCountdownAnimationComplete()
	{
        //enable the tooltip and play its into animation
		swipeTooltipObject.SetActive (true);
		tooltipController.Show();
        //begin gameplay
		beginGameplay ();
	}

    public void HideTooltip()
    {
        tooltipController.Hide();
    }

    public void beginGameplay()
	{
        //create player
		playerObject = Instantiate (playerPrefab, gameStageParent);
		playerObject.GetComponent<PlayerController>().Init(this);
        //create first safe
        GameObject safeObject = Instantiate(safePrefab, gameStageParent);
        safeObject.GetComponent<SafeController>().Init(this);
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
	}

	public List<GameObject> SpawnMultiple (int numToSpawn, GameObject gameObject)
	{
        List<GameObject> objectList = new List<GameObject>();
		for (int i = 0; i < numToSpawn; i++) 
		{
			GameObject obj = SpawnGameObjectAtRandomPosition(gameObject);
			objectList.Add(obj);
		}
		return objectList;
	}

    public GameObject SpawnGameObjectAtRandomPosition(GameObject gameObject)
    {
        Vector3 screenPosition = GetRandomLocationOnscreen();
        return SpawnGameObjectAtPosition(gameObject, screenPosition);
    }

    public GameObject SpawnGameObjectAtPosition(GameObject gameObject, Vector2 position)
    {
        Vector3 pos3 = new Vector3(position.x, position.y, 0);
        GameObject obj = Instantiate(gameObject, Vector3.zero, Quaternion.identity, gameStageParent);
        obj.transform.localPosition = pos3;
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        if (gameObject.tag != "Safe")
        {
            obj.transform.localScale = new Vector3(3, 3, 1);
        }
        if (gameObject == coinPrefab)
        {
            obj.transform.localScale = new Vector3(30, 30, 1);
            coinList.Add(obj);
        }
        else if (gameObject == minePrefab)
        {
            mineList.Add(obj);
        }
        return obj;
    }

    public static Vector3 GetRandomLocationOnscreen ()
	{

        float horizontalBuffer = Tools.screenWidth / 10;
        float verticalBuffer = Tools.screenHeight / 10;

        float halfwidth = Tools.screenWidth / 2;
        float halfheight = Tools.screenHeight / 2;

		Vector3 position = new Vector3 (
            Random.Range (-halfwidth+horizontalBuffer, halfwidth-horizontalBuffer), 
            Random.Range (-halfheight+verticalBuffer, halfheight-verticalBuffer),
            0);
		return position;
	}

	public void HandleSafeDestroyed(int numCoins, Transform safeLocation)
	{
		//spawn multiple coins	
        numCoins = numCoinsInSafe;	
		for (int i = 0; i < numCoins; i++)
        {
            SpawnGameObjectAtPosition (coinPrefab, safeLocation.localPosition);
        }
		AddSafe();
	}
	

	public void CheckCoinsCollected(GameObject coin)
	{
        currentCoinCount++;
        uiController.SetCoinText(currentCoinCount);
        coinList.Remove (coin);
	}

	public void AddSafe()
	{
        //Debug.Log("Adding safe");
        GameObject safe = SpawnGameObjectAtRandomPosition(safePrefab);
        safe.GetComponent<SafeController>().Init(this);

	} 

    //!!! This function doesn't take safes or explosion puffs into accound
	public void DestroyAllItemsOnscreen()
	{
        //destroy coins
        for (int i = coinList.Count - 1; i >= 0; i--)
        {
            Destroy(coinList[i].gameObject);
        }
        //destroy mines
        for (int i = mineList.Count-1; i >= 0; i-- )
        {
            Destroy (mineList[i].gameObject);
        }
        //destroy player
		Destroy (playerObject);
	}

	public void HandlePlayerDestroyed()
	{
        timeController.handlePlayerDestroyed();
        SavePlayerPrefs();
		StartCoroutine (ShowEndgameScreenAfterSeconds (delayBeforeEndGameScreenAppears));
	}

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("lastScore", currentCoinCount);
        PlayerPrefs.SetInt("currentCoins", PlayerPrefs.GetInt("currentCoins", 0) + currentCoinCount);
        if (PlayerPrefs.GetInt("bestScore") < currentCoinCount)
        {
            PlayerPrefs.SetInt("bestScore", currentCoinCount);
        }
    }

	IEnumerator ShowEndgameScreenAfterSeconds (float waitTime) 
	{
        yield return new WaitForSeconds(waitTime);
        endgameScreenController.PopulateEndgameScreenContent(
            currentCoinCount.ToString(),
            PlayerPrefs.GetInt("bestScore").ToString(),
            PlayerPrefs.GetInt("currentCoins").ToString());
        endgameScreenController.gameObject.SetActive (true);
	}

    public int GetCoinCount()
    {
        return currentCoinCount;
    }

}

