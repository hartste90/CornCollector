using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseConfirmationPanelController : MonoBehaviour {

    public Text subject;
    public Text description;

    public void ShowPurchaseFailed(string message)
    {
        subject.text = "Purchase Incomplete";
        description.text = message;
        Show();
    }

    public void ShowPurchaseSuccess(string message)
    {
        subject.text = "Purchase Complete";
        description.text = message;
        Show();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
