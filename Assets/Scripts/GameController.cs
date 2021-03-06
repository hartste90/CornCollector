﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class  GameController : MonoBehaviour
{
    public bool isVerbose;
    public static bool verbose = true;
    //magic numbers
    public float delayBeforeEndGameScreenAppears = .7f;
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
    public Transform uiCoinTargetTransform;
    public UIController uiController;
    public TitleScreenController titleScreenController;
	public EndgameScreenController endgameScreenController;
	public ContinueScreenController continueScreenController;
    public CountdownController countdownController;
    public BackgroundMusicController backgroundMusicController;
    public SoundEffectsController soundEffectsController;
    public JITEndscreenController jITEndscreenController;
    public InterstitialController interstitialController;
    public LevelUpPanelController levelUpPanelController;
    public IAPManager iAPManager;
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
    public List<MineController> mineList;
    public List<GameObject> safeList;
    public List<GameObject> explosionPuffList;
    public int currentCoinCount = 0;

    private static float horizontalBuffer;
    private static float verticalBuffer;

    private static float halfwidth;
    private static float halfheight;



    public void Awake()
    {
        string userId = AnalyticsSessionInfo.userId;
        Analytics.CustomEvent("gameLoad", new Dictionary<string, object>
        {
            { "userId", userId }
        });
        Analytics.CustomEvent("gameLoadNoID");

        isVerbose = verbose;
        //record device dimensions
		Tools.screenWidth = Screen.width;
		Tools.screenHeight = Screen.height;
        //instantiate lists
        coinList = new List<GameObject>();
        mineList = new List<MineController>();
        safeList = new List<GameObject>();
        explosionPuffList = new List<GameObject>();
        interstitialController.gameObject.SetActive(true);
        levelUpPanelController.gameObject.SetActive(true);
    }

    // a test function to trigger custom functionality for debugging
	public void TestFunc()
	{
        PlayerPrefManager.DeleteAll();
        PlayerPrefManager.SetPinkCount(21);
        //PlayerPrefManager.AddPinkCoins(17);
        //currentCoinCount = 900;
        //currentCoinCount = 1000;
        //currentCoinCount = 100;
        //currentCoinCount = 101;
        //currentCoinCount = 99;
        //currentCoinCount = 0;
        currentCoinCount = 530;
        //playerController.OnHitMine();
        this.LevelUp();


    }


    void Start()
	{

        //check/set special session elements
        PlayerPrefManager.IncrementNumLogins();
        if ((int)PlayerPrefManager.GetFirstLoginDate()["year"] == 0)
        {
            PlayerPrefManager.SetFirstLoginDate(System.DateTime.Now);
        }
        GameModel.ResetSafes();
        GameModel.numAttempts = 1;
        GameModel.userLevel = 1;
        GameModel.SetGoldCoinCount(0);
        GameModel.DisableShipInput();


        //setup private links
        tooltipController = swipeTooltipObject.GetComponent<TooltipController>();
        timeController = GetComponent<TimeController>();
        nextSafeCoinRequirement = 50;
        //screen size calculations
        horizontalBuffer = Tools.screenWidth / 10;
        verticalBuffer = Tools.screenHeight / 10;
        halfwidth = Tools.screenWidth / 2;
        halfheight = Tools.screenHeight / 2;
        uiController.HideGameUI();
        SetupGameStart();

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
        //enable the tooltip and play its into animation
        tooltipController.Show();
    }

    public void OnPlayerBeginsMovement()
    {
        playerController.BeginDropExhaust();
        tooltipController.Hide();
        titleScreenController.HideTitleScreen();
        uiController.ShowGameUI();
        if (GetCoinCount() == 0 || GameModel.hasJustContinued == true)
        {
            Analytics.CustomEvent("startedGameplay", new Dictionary<string, object>
            {
                { "userId", AnalyticsSessionInfo.userId },
                { "attempts", GameModel.numAttempts},
                { "replays", GameModel.numReplays },
                { "time", Time.time }

            });
            backgroundMusicController.fadeInBackgroundMusic();
            GameModel.hasJustContinued = false;
        }

    }

    public void beginGameplay()
	{
        GameModel.canCollectCoins = true;
        GameModel.shouldReplaceSafes = true;
        //create player
		playerObject = Instantiate (playerPrefab, gameStageParent);
        playerController = playerObject.GetComponent<PlayerController>();
        playerController.playerStartPositionAnchor = playerStartPositionAnchor;
        playerController.Init(this);
        playerController.AnimateIntro();
        //create safes for number of coins
        numSafes = GameModel.userLevel;
        //create first safe
        GameObject safeObject = Instantiate(safePrefab, gameStageParent);
        safeObject.transform.localPosition = safeStartPositionAnchor.localPosition;
        safeList.Add(safeObject);
        safeObject.GetComponent<Animator>().SetTrigger("ShowImmediate");
        safeObject.GetComponent<SafeController>().collider.enabled = true;
        safeObject.GetComponent<SafeController>().Init(this, numCoinsInSafe);
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
        Analytics.CustomEvent("pressedReplay", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId },
            { "attempts", GameModel.numAttempts},
            { "replays", GameModel.numReplays },
            { "time", Time.time },
            { "score", GameModel.GetGoldCoinCount()},

        });
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DestroyAllItemsOnscreen();
        currentCoinCount = 0;
        uiController.ResetUI();
        jITEndscreenController.HideCoinPanel(true);
        GameModel.numSafes = 1;
        GameModel.userLevel = 1;
        GameModel.numReplays++;
        endgameScreenController.endScreenExitCallback = ShowInterstitial;
        endgameScreenController.Hide();

    }

    public void SetupForLevelStart()
    {
        Time.timeScale = 1.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DestroyAllItemsOnscreen();
        playerController.DestroySelf();
        uiController.HideGameUI();
        SetupGameStart();
    }

    public void ShowInterstitial()
    {
        if (interstitialController.IsReady() == true){
            interstitialController.completeCallback = InterstitialCompleteCallback;
            interstitialController.ShowTip();
        }
        else
        {
            SetupGameStart();
        }

    }

    public void InterstitialCompleteCallback()
    {
        SetupGameStart();
    }

    public void SetupGameStart()
    {
        titleScreenController.gameObject.SetActive(true);
        titleScreenController.ShowTitleScreen();
        ShowSwipeTooltip();
        beginGameplay();
    }

    public void ContinueGame()
    {
        //destroy all objects currently on stage
        DestroyAllItemsOnscreen();
        //unslow time
        Time.timeScale = 1.0f;
        //reset coins (since they have already been added up in endscreen)
        //currentCoinCount = 0;
        //uiController.ResetUI();
        jITEndscreenController.HideCoinPanel(true);
        //replay game start UI tooltip/tutorial
        endgameScreenController.endScreenExitCallback = beginGameplay;
        endgameScreenController.Hide();
        //should be callback for when endgame screen is gone
        //beginGameplay();
        GameModel.hasJustContinued = true;
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

        if (gameObject == coinPrefab)
        {
            //obj.transform.localScale = new Vector3(30, 30, 1);
            obj.GetComponent<GravitateToTarget>().SetTarget(uiCoinTargetTransform);
            coinList.Add(obj);
        }
        else if (gameObject == minePrefab)
        {
            MineController mc = obj.GetComponent<MineController>();
            mineList.Add(mc);
            mc.gameController = this;
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
        if (GameModel.canCollectCoins == true) 
        {
            Analytics.CustomEvent("safeDestroyed", new Dictionary<string, object>
            {
                { "userId", AnalyticsSessionInfo.userId },
                { "numSafes", GameModel.numSafes },
                { "attempts", GameModel.numAttempts },
                { "replays", GameModel.numReplays },
                { "time", Time.time }

            });
            playerController.ShowCoinCollectUpdate(numCoins);
            soundEffectsController.PlaySafeBustSound();
        }
		//spawn multiple coins	
        numCoins = numCoinsInSafe;	
		for (int i = 0; i < numCoins; i++)
        {
            SpawnGameObjectAtPosition (coinPrefab, safeLocation.localPosition);
        }
        if (GameModel.shouldReplaceSafes)
        {
            AddSafe();
        }
		
	}
	

	public void CheckCoinsCollected(GameObject coin)
	{
        if (GameModel.canCollectCoins == true)
        {
            currentCoinCount++;
            soundEffectsController.PlayCoinCollectedSound();
            //check if we need to add another safe
            if (currentCoinCount >= nextSafeCoinRequirement)
            {
                int shouldHave = FindNumSafesToCreate();
                if (safeList.Count < shouldHave && GameModel.shouldReplaceSafes == true)
                {
                    //levelup
                    this.LevelUp();
                }
            }
        }
        uiController.SetCoinText(currentCoinCount);
        coinList.Remove (coin);


    }

    private void LevelUp()
    {
        if (GameModel.shouldReplaceSafes == false) { return; }
        //turn off input
        GameModel.DisableShipInput();
        //detonate all mines (making sure explosions are created)
        for (int i = this.mineList.Count - 1; i >= 0; i--)
        {
            mineList[i].MineExplode();
            //mineList[i].MineExplode();
        }
        //disable collider on player
        playerController.SetInvincible(true);
        //detonate all safes
        GameModel.shouldReplaceSafes = false;
        for (int i = this.safeList.Count - 1; i >= 0; i--)
        {
            safeList[i].GetComponent<SafeController>().HandleHitByExplosion();
        }
        //create explosive force
        for (int i = 0; i < explosionPuffList.Count; i++)
        {
            AddExplosionForce2D(explosionPuffList[i].GetComponent<Rigidbody2D>(), 500f, playerObject.transform.position, 10000f);
        }
        //slow time
        timeController.SlowTime();
        //show levelup copy
        GameModel.userLevel++;
        levelUpPanelController.panelCompleteAnimationCallback = handleLevelupPanelAnimationComplete;
        levelUpPanelController.Show(GameModel.userLevel);
        //Debug.Break();

        //AddSafe();
        //GameModel.AddSafe();

    }

    public void handleLevelupPanelAnimationComplete()
    {
        SetupForLevelStart();

    }

    public void CreatePhysicalExplosion()
    {
        Vector3 explosionPos = playerObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 10000000f);
        Debug.Log(colliders.Length);
        foreach (Collider hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
                AddExplosionForce2D( rb, 10f, explosionPos, 100f);
        }
    }

    public void AddExplosionForce2D(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        Vector3 dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 force = dir.normalized * explosionForce * wearoff;
        body.AddForce(force);
    }

    public void AddSafe()
	{
        //Debug.Log("Adding safe");
        GameObject safe = SpawnGameObjectAtRandomPosition(safePrefab);
        safeList.Add(safe);
        safe.GetComponent<SafeController>().Init(this, numCoinsInSafe);

	} 

    //!!! This function doesn't take safes or explosion puffs into accound
	public void DestroyAllItemsOnscreen()
	{
        //destroy coins
        for (int i = coinList.Count - 1; i >= 0; i--)
        {
            Destroy(coinList[i].gameObject);
        }
        coinList = new List<GameObject>();
        //destroy mines
        for (int i = mineList.Count-1; i >= 0; i-- )
        {
            mineList[i].DestroySelf(true);
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
        Analytics.CustomEvent("playerDestroyed", new Dictionary<string, object>
            {
                { "userId", AnalyticsSessionInfo.userId },
                { "attempts", GameModel.numAttempts},
                { "replays", GameModel.numReplays },
                { "score", GameModel.GetGoldCoinCount()},
                { "time", Time.time }

            });
        GameModel.DisableShipInput();
        GameModel.canCollectCoins = false;
        StopGameCoinsFromGravitating();
        timeController.SlowTime();
        SavePlayerPrefs();
        soundEffectsController.PlayPlayerDeathSound();
        backgroundMusicController.fadeOutBackgroundMusic();
        StartCoroutine(ShowEndgameScreenAfterSeconds (delayBeforeEndGameScreenAppears));
	}

    private void StopGameCoinsFromGravitating()
    {
        foreach (GameObject go in coinList)
        {
            go.GetComponent<GravitateToTarget>().SetShouldGravitate(false);
        }
    }

    private void SavePlayerPrefs()
    {
        if (PlayerPrefManager.GetBestScore() < currentCoinCount)
        {
            Analytics.CustomEvent("newBestScore", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId },
            { "attempts", GameModel.numAttempts},
            { "replays", GameModel.numReplays },
            { "time", Time.time },
            { "newBest", currentCoinCount},
            { "oldBest", PlayerPrefManager.GetBestScore()},

        });
            PlayerPrefManager.SetBestScore(currentCoinCount);
        }
    }

	IEnumerator ShowEndgameScreenAfterSeconds (float waitTime) 
	{
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 1f;
        uiController.HideGameUI();
        endgameScreenController.PopulateEndgameScreenContent(
            currentCoinCount.ToString(),
            PlayerPrefManager.GetBestScore().ToString());
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

