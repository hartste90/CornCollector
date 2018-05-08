using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Text bestScoreText;
	public Text lastScoreText;


	// Use this for initialization
	void Start () {
		bestScoreText.text = PlayerPrefs.GetInt ("bestScore", 0).ToString ();
		lastScoreText.text = PlayerPrefs.GetInt ("lastScore", 0).ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void handlePlayButtonPressed()
	{
		Initiate.Fade("MainGame",Color.black,5f);
//		SceneManager.LoadScene ("MainGame");

	}
}
