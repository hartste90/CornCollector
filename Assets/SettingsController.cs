using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour {

    public GameObject settingsPanel;

    private bool isShowingSettings = false;
    // Use this for initialization
    void Start()
    {

    }

    public void ShowSettingsPanel()
    {
        isShowingSettings = true;
        settingsPanel.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        isShowingSettings = false;
        settingsPanel.SetActive(false);
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
