using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateGameController : MonoBehaviour {

    public GameObject primaryQuestionPanel;
    public GameObject affirmativeFollowupPanel;
    public GameObject negativeFollowupPanel;
    public GameObject affirmativeConclusionPanel;
    public GameObject negativeConclusionPanel;
    public GameObject feedbackPanel;

    private Animator animator;
    private GameObject nextPanelToShow;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ShowPrimaryQuestionPanel();
    }

    public void ShowPrimaryQuestionPanel()
    {
        //hide other panels
        HideAllPanels();
        //show primary question panel
        primaryQuestionPanel.SetActive(true);
        //animate button in
        animator.SetTrigger("Show");
    }

    public void GoToNegativeFollowupPanel()
    {
        //animate off
        animator.SetTrigger("Hide");
        //setup next panel as negative followup
        nextPanelToShow = negativeFollowupPanel;
    }

    public void GoToAffirmativeFollowupPanel()
    {
        animator.SetTrigger("Hide");
        nextPanelToShow = affirmativeFollowupPanel;
    }


    public void GoToAffirmativeConclusionPanel()
    {
        animator.SetTrigger("Hide");
        nextPanelToShow = affirmativeConclusionPanel;
    }


    public void GoToNegativeConclusionPanel()
    {
        animator.SetTrigger("Hide");
        nextPanelToShow = negativeConclusionPanel;
    }




    public void HandleButtonHideComplete()
    {
        HideAllPanels();
        if (nextPanelToShow != null)
        {
            nextPanelToShow.SetActive(true);
            nextPanelToShow = null;
            animator.SetTrigger("Show");
        }
    }

    
    public void ShowFeedbackPanel()
    {
        feedbackPanel.SetActive(true);
    }

    public void HideFeedbackPanel()
    {
        feedbackPanel.SetActive(false);
    }


    public void GoToAppStorePage()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id="+Application.productName);
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/"+Application.productName);
#else
        Debug.Log("Won't open app store");
#endif
    }

    public void HideAllPanels()
    {
        primaryQuestionPanel.SetActive(false);
        affirmativeFollowupPanel.SetActive(false);
        negativeFollowupPanel.SetActive(false);
        affirmativeConclusionPanel.SetActive(false);
        negativeConclusionPanel.SetActive(false);
    }
}
