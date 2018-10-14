using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public void PurchasePackage (int numCoins)
    {
        AddPinkCoins(numCoins);
    }

    public void HandleRemoveAdsButtonPressed ()
    {
        Debug.Log("STUB: purchase REMOVE ADS package");
    }


    private void AddPinkCoins(int num)
    {
        PlayerPrefManager.AddPinkCoins(num);
    }


}
