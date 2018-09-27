﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

    public GameObject minePrefab;
    public Transform mineParent;
    public GameObject gameName;

    private GameObject mine;

    private void Awake()
    {
        mine = Instantiate(minePrefab, mineParent);
        mine.transform.localPosition = Vector3.zero;
    }

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
