using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    public GameObject mine;
    public GameObject gameName;

	public void ShowTitleScreen()
    {
        mine.SetActive(true);
        gameName.SetActive(true);
    }

    public void HideTitleScreen()
    {
        mine.SetActive(false);
        gameName.SetActive(false);
    }
}
