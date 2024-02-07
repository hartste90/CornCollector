using UnityEngine;
using UnityEngine.Events;

public class IAPManager : MonoBehaviour
{

    public EndgameScreenController endgameScreenController;


    private void Start()
    {
        //purchaseListener.onPurchaseComplete.AddListener(HandlePurchaseSuccess);
        //purchaseListener.onPurchaseFailed.AddListener(HandlePurchaseFailure);
    }

    public void PurchasePackage (int numCoins)
    {
        AddPinkCoins(numCoins);
    }


    private void AddPinkCoins(int num)
    {
        PlayerPrefManager.AddPinkCoins(num);
    }
    
}
