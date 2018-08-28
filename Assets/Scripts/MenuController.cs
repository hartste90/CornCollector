﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Text bestScoreText;
	public Text lastScoreText;
    public Rigidbody2D playerrb;
    public float playerSpeed = 2.5f;


	// Use this for initialization
	void Start () {
		bestScoreText.text = PlayerPrefs.GetInt ("bestScore", 0).ToString ();
		lastScoreText.text = PlayerPrefs.GetInt ("lastScore", 0).ToString ();
        playerrb.velocity = Vector3.right * playerSpeed;
	}

	public void handlePlayButtonPressed()
	{
        Initiate.Fade("MainGame", Color.black, 5f);
	}
}
