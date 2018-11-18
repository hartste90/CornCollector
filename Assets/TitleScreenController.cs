using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour {

    public GameObject minePrefab;
    public Transform mineParent;
    public GameObject gameName;
    public GameObject titleScreenUI;
    public CurrentLevelPanelController currentLevelPanelController;

    private GameObject mine;

    private void Awake()
    {
        mine = Instantiate(minePrefab, mineParent);
        mine.transform.localPosition = Vector3.zero;
    }

    public void ShowTitleScreen()
    {
        mine.SetActive(false);
        gameName.SetActive(true);
        titleScreenUI.SetActive(true);
        currentLevelPanelController.Show(GameModel.userLevel);
    }

    public void HideTitleScreen()
    {
        mine.SetActive(false);
        gameName.SetActive(false);
        titleScreenUI.SetActive(false);
        currentLevelPanelController.Hide();
    }
}
