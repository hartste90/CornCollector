using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

    private bool isShowingCredits = false;
	// Use this for initialization
	void Start () {
		
	}
	
	public void ShowCreditsPanel()
    {
        GameModel.DisableShipInput();
        isShowingCredits = true;
        gameObject.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        GameModel.EnableShipInput();
        isShowingCredits = false;
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
