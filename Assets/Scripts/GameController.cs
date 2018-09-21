﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class  GameController : MonoBehaviour
{
    public bool isVerbose;
    public static bool verbose = true;
    //magic numbers
    public float delayBeforeEndGameScreenAppears = .7f;
    public int userLevel = 1;
    public int numSafes = 1;
    public float minimumSwipeDistance = 0f;
    public float gameSpeed = 2.5f;
    public int numCoinsInSafe = 10;
    public bool debugAllowAds = true;

    //links
    public GameObject swipeTooltipObject;
    public GameObject playerPrefab;
    public GameObject coinPrefab;
    public GameObject minePrefab;
    public GameObject safePrefab;
    public Transform gameStageParent;
    public Transform playerStartPositionAnchor;
    public Transform safeStartPositionAnchor;
    public UIController uiController;
    public TitleScreenController titleScreenController;
	public EndgameScreenController endgameScreenController;
	public ContinueScreenController continueScreenController;
    public CountdownController countdownController;
    public BackgroundMusicController backgroundMusicController;
    public SoundEffectsController soundEffectsController;

    //private links
    private TimeController timeController;
    private TooltipController tooltipController;
    private PlayerController playerController;
    private GameObject playerObject;

    //safe stuff
    private Dictionary<int, int> safeDictionary = new Dictionary<int, int>
    {
        {0, 1},
        {50, 2},
        {200, 3},
        {400, 4},
        {650, 5},
        {900, 6},
        {1200, 7},
        {1600, 8},
        {2000, 9},
        {2500, 10},
    };
    private int nextSafeCoinRequirement;

    //tracking
    public float lastTimePlayerWatchedVideo = -3000f;
	public List<GameObject> coinList;
	public List<GameObject> mineList;
    public List<GameObject> safeList;
    public List<GameObject> explosionPuffList;
    public int currentCoinCount = 0;

    private static float horizontalBuffer;
    private static float verticalBuffer;

    private static float halfwidth;
    private static float halfheight;

    public void Awake()
    {
        isVerbose = verbose;
        //record device dimensions
		Tools.screenWidth = Screen.width;
		Tools.screenHeight = Screen.height;
        //instantiate lists
        coinList = new List<GameObject>();
        mineList = new List<GameObject>();
        safeList = new List<GameObject>();
        explosionPuffList = new List<GameObject>();
    }

    // a test function to trigger custom functionality for debugging
	public void TestFunc()
	{
        PlayerPrefs.DeleteAll();
        //GameModel.SetPinkCoinCount(7);
        //soundEffectsController.PlayPlayerDeathSound();
        currentCoinCount = 932;
        HandlePlayerDestroyed();


    }


    void Start()
	{
        GameModel.numAttempts = 1;
        GameModel.SetGoldCoinCount(0);
        LoadPlayerPrefs();

        //setup private links
        tooltipController = swipeTooltipObject.GetComponent<TooltipController>();
        timeController = GetComponent<TimeController>();
        nextSafeCoinRequirement = 50;
        ShowBeginUI();
        //screen size calculations
        horizontalBuffer = Tools.screenWidth / 10;
        verticalBuffer = Tools.screenHeight / 10;
        halfwidth = Tools.screenWidth / 2;
        halfheight = Tools.screenHeight / 2;
        titleScreenController.gameObject.SetActive(true);
        titleScreenController.ShowTitleScreen();
        ShowSwipeTooltip();
        beginGameplay();

    }

    private void LoadPlayerPrefs()
    {
        GameModel.SetPinkCoinCount(PlayerPrefs.GetInt("pinkCoinCount", 0));
    }

    private void ShowBeginUI()
    {
        //hide the end game screen if it's been shown
        endgameScreenController.gameObject.SetActive(false);
        uiController.ShowUI();
        //countdownController.ShowCountdown();

    }

    //DEPRECATED WHEN MOVED TITLE SCREEN TO MAIN GAME
    //shows the tooltip at the beginning of the game
    public void HandleCountdownAnimationComplete()
	{
        ShowSwipeTooltip();
        //begin gameplay
		beginGameplay ();
	}

    private void ShowSwipeTooltip()
    {
        swipeTooltipObject.SetActive(true);
        //enable the tooltip and play its into animation
        tooltipController.Show();
    }

    public void OnPlayerBeginsMovement()
    {
        tooltipController.Hide();
        backgroundMusicController.playBackgroundMusic();
        titleScreenController.HideTitleScreen();
    }

    public void beginGameplay()
	{
        GameModel.canCollectCoins = true;
        //create player
		playerObject = Instantiate (playerPrefab, gameStageParent);
        playerController = playerObject.GetComponent<PlayerController>();
        playerController.playerStartPositionAnchor = playerStartPositionAnchor;
        playerController.Init(this);
        //create safes for number of coins
        numSafes = FindNumSafesToCreate();
        //create first safe
        GameObject safeObject = Instantiate(safePrefab, gameStageParent);
        safeObject.transform.localPosition = safeStartPositionAnchor.localPosition;
        safeList.Add(safeObject);
        safeObject.GetComponent<SafeController>().Init(this);
        for (int i = 1; i < numSafes; i++)
        {
            AddSafe();
        }
    }

    private int FindNumSafesToCreate()
    {

        List<int> coinReq = new List<int>(safeDictionary.Keys);
        //List<int> safeCount = new List<int>(safeDictionary.Values);

        int nmSafes = safeDictionary[coinReq[0]];
        for (int i = 0; i < coinReq.Count; i++)
        {
            int nextCoinCount = coinReq[i];
            if (currentCoinCount >=  nextCoinCount)
            {
                numSafes = safeDictionary[nextCoinCount];
                //Debug.Log(currentCoinCount + ":" + coinNum);
            }
        }

        return numSafes;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ContinueGame()
    {

        //destroy all objects currently on stage
        DestroyAllItemsOnscreen();
        //unslow time
        Time.timeScale = 1.0f;
        //keep coin count & update UI (might change with cost of 
        uiController.SetCoinText(currentCoinCount);
        //replay game start UI tooltip/tutorial
        ShowBeginUI();
        beginGameplay();
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
        //if (gameObject.tag != "Safe")
        //{
        //    obj.transform.localScale = new Vector3(3, 3, 1);
        //    Debug.LogError("WHAT IS THIS OBJECT");
        //}
        if (gameObject == coinPrefab)
        {
            obj.transform.localScale = new Vector3(30, 30, 1);
            coinList.Add(obj);
        }
        else if (gameObject == minePrefab)
        {
            mineList.Add(obj);
            obj.GetComponent<MineController>().gameController = this;
        }
        return obj;
    }

    public static Vector3 GetRandomLocationOnscreen ()
	{
		return new Vector3 (
            Random.Range (-halfwidth+horizontalBuffer, halfwidth-horizontalBuffer), 
            Random.Range (-halfheight+verticalBuffer, halfheight-verticalBuffer),
            0);
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
        if (GameModel.canCollectCoins == true)
        {
            currentCoinCount++;
        }
        uiController.SetCoinText(currentCoinCount);
        coinList.Remove (coin);
        //check if we need to add another safe
        if(currentCoinCount >= nextSafeCoinRequirement)
        {
            int shouldHave = FindNumSafesToCreate();
            if (safeList.Count < shouldHave)
            {
                AddSafe();
            }
        }

    }

	public void AddSafe()
	{
        //Debug.Log("Adding safe");
        GameObject safe = SpawnGameObjectAtRandomPosition(safePrefab);
        safeList.Add(safe);
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
            mineList[i].GetComponent<MineController>().DestroySelf(true);
        }
        //destrxy safes
        for (int i = safeList.Count - 1; i >= 0; i--)
        {
            safeList[i].GetComponent<SafeController>().DestroySelf(true);
        }
        //destroy explosionPuffs
        for (int i = explosionPuffList.Count - 1; i >= 0; i--)
        {
            explosionPuffList[i].GetComponent<ExplosionPuffController>().DestroySelf(true);
        }

        //destroy player
        Destroy(playerObject);
	}

	public void HandlePlayerDestroyed()
	{
        //GameModel.canCollectCoins = false;
        timeController.handlePlayerDestroyed();
        SavePlayerPrefs();
        soundEffectsController.PlayPlayerDeathSound();
		StartCoroutine (ShowEndgameScreenAfterSeconds (delayBeforeEndGameScreenAppears));
	}

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("lastScore", currentCoinCount);
        PlayerPrefs.SetInt("currentCoins", PlayerPrefs.GetInt("currentCoins", 0) + currentCoinCount);
        PlayerPrefs.SetInt("pinkCoinCount", GameModel.GetPinkCoinCount());
        if (PlayerPrefs.GetInt("bestScore") < currentCoinCount)
        {
            PlayerPrefs.SetInt("bestScore", currentCoinCount);
        }
    }

	IEnumerator ShowEndgameScreenAfterSeconds (float waitTime) 
	{
        yield return new WaitForSeconds(waitTime);
        uiController.HideUI();
        endgameScreenController.PopulateEndgameScreenContent(
            currentCoinCount.ToString(),
            PlayerPrefs.GetInt("bestScore").ToString(),
            PlayerPrefs.GetInt("currentCoins").ToString());
        endgameScreenController.gameObject.SetActive (true);
        endgameScreenController.ShowEndGameScreen();
	}

    public int GetCoinCount()
    {
        return currentCoinCount;
    }

    public void HandleContinueCoinButtonPressed()
    {


    }

    public void HandleStartOverButtonPressed()
    {
        ResetScene();

    }

}

