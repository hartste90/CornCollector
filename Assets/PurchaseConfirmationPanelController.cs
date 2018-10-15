using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseConfirmationPanelController : MonoBehaviour {

    public void ShowPurchaseConfirmationPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePurchaseConfirmationPanel()
    {
        gameObject.SetActive(false);
    }

}
