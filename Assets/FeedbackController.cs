using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public enum FeedbackType
{
    DIFFICULTY = 0,
    CONFUSING,
    SHOP,
    BUG
}

public class FeedbackController : MonoBehaviour {


    public void SendFeedbackByType(int type){
        SendAnalyticsEvent((FeedbackType)type);
    }

    private void SendAnalyticsEvent(FeedbackType type)
    {
        Analytics.CustomEvent("feedbackQuickResponse", new Dictionary<string, object>
        {
            { "feedbackType", type }
        });

    }


    public void SendEmail()
    {
        string email = "steven.hart282@gmail.com";
        string subject = MyEscapeURL("Bomb Buster Feedback");
        string body = MyEscapeURL("\nUser feedback:\n\n");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }

    public void ShowFeedbackPanel()
    {
        GameModel.DisableShipInput();
        gameObject.SetActive(true);
    }

    public void HideFeedbackPanel()
    {
        GameModel.EnableShipInput();
        gameObject.SetActive(false);
    }

}