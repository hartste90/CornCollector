using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class RemoveAdsButtonController : MonoBehaviour {

    public Button button;
    public GameObject purchasedPanel;

    public CanvasGroup canvasGroup;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        CodelessIAPStoreListener.Instance.initializationCompleteCallback = OnPurchasingInitialized;
    }

    public void OnPurchasingInitialized()
    {
        CheckPurchases();
    }

    private void CheckPurchases()
    {
        //if they have purchased remove ads/cover the button
        if (CodelessIAPStoreListener.Instance.GetProduct("remove.ads").hasReceipt)
        {
            RemoveAdsPurchased();
        }
    }

    public void RemoveAdsPurchased()
    {
        canvasGroup.alpha = .1f;
        canvasGroup.interactable = false;
        //button.gameObject.SetActive(false);
        //button.interactable = false;
        //purchasedPanel.SetActive(true);
    }

}
