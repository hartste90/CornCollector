using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SettingsController : MonoBehaviour {


    private bool isShowingSettings = false;
    // Use this for initialization
    void Start()
    {

    }

    public void ShowSettingsPanel()
    {
        Analytics.CustomEvent("showSettings", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });

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

    public void ToggleSettingsPanel()
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
