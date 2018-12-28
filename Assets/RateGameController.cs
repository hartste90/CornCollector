using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RateGameController : MonoBehaviour {

    public GameObject primaryQuestionPanel;
    public GameObject affirmativeFollowupPanel;
    public GameObject negativeFollowupPanel;
    public GameObject affirmativeConclusionPanel;
    public GameObject negativeConclusionPanel;
    public FeedbackController feedbackController;

    private Animator animator;
    private GameObject nextPanelToShow;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowRateGamePanel()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }

    public void ShowPrimaryQuestionPanel()
    {

        Analytics.CustomEvent("showRatePrimaryQuestion", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });

        //hide other panels
        HideAllPanels();
        //show primary question panel
        primaryQuestionPanel.SetActive(true);
        //animate button in
        animator.SetTrigger("Show");
    }

    public void GoToNegativeFollowupPanel()
    {
        Analytics.CustomEvent("showRateNegativeFollowup", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });
        //animate off
        animator.SetTrigger("Hide");
        //setup next panel as negative followup
        nextPanelToShow = negativeFollowupPanel;
    }

    public void GoToAffirmativeFollowupPanel()
    {
        Analytics.CustomEvent("showRatePositiveFollowup", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });
        animator.SetTrigger("Hide");
        nextPanelToShow = affirmativeFollowupPanel;
    }


    public void GoToAffirmativeConclusionPanel()
    {
        Analytics.CustomEvent("showRatePositiveConclusion", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });
        animator.SetTrigger("Hide");
        nextPanelToShow = affirmativeConclusionPanel;
    }


    public void GoToNegativeConclusionPanel()
    {
        Analytics.CustomEvent("showRateNegativeConclusion", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });
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
        feedbackController.ShowFeedbackPanel();
    }

    public void HideFeedbackPanel()
    {
        feedbackController.HideFeedbackPanel();
    }


    public void GoToAppStorePage()
    {
        Analytics.CustomEvent("goToAppStore", new Dictionary<string, object>
        {
            { "userId", AnalyticsSessionInfo.userId }
        });
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
