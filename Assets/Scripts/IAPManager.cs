using UnityEngine;

public class IAPManager : MonoBehaviour
{

    public PurchaseConfirmationPanelController removeAdsPurchaseConfirmationPanelController;

    public void PurchasePackage (int numCoins)
    {
        AddPinkCoins(numCoins);
    }

    public void HandleRemoveAdsButtonPressed ()
    {
        OnRemoveAdsPurchased();
    }


    private void AddPinkCoins(int num)
    {
        PlayerPrefManager.AddPinkCoins(num);
    }

    public void OnRemoveAdsPurchased()
    {
        removeAdsPurchaseConfirmationPanelController.ShowPurchaseConfirmationPanel();
    }


}
