using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

    public UIController uiController;

    private bool isShowingCredits = false;


	public void ShowCreditsPanel()
    {
        GameModel.DisableShipInput();
        isShowingCredits = true;
        uiController.HideTitleUI();
        gameObject.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        GameModel.EnableShipInput();
        isShowingCredits = false;
        uiController.ShowTitleUI();
        gameObject.SetActive(false);
    }

    public void ToggleCreditsPanel()
    {
        if (isShowingCredits)
        {
            HideCreditsPanel();

        }
        else
        {
            ShowCreditsPanel();
        }
    }
}
