using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour {

    public GameObject creditsPanel;

    private bool isShowingCredits = false;
	// Use this for initialization
	void Start () {
		
	}
	
	public void ShowCreditsPanel()
    {
        isShowingCredits = true;
        creditsPanel.SetActive(true);
    }

    public void HideCreditsPanel()
    {
        isShowingCredits = false;
        creditsPanel.SetActive(false);
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
