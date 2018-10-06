using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour {


    private bool isShowingSettings = false;
    // Use this for initialization
    void Start()
    {

    }

    public void ShowSettingsPanel()
    {
        GameModel.DisableShipInput();
        isShowingSettings = true;
        gameObject.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        GameModel.EnableShipInput();
        isShowingSettings = false;
        gameObject.SetActive(false);
    }

    public void ToggleCreditsPanel()
    {
        if (isShowingSettings)
        {
            HideSettingsPanel();

        }
        else
        {
            ShowSettingsPanel();
        }
    }
}
