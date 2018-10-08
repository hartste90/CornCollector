using UnityEngine;

public class IAPManager : MonoBehaviour
{
    public void PurchasePackage (int packageId)
    {
        Debug.Log("STUB: purchase package: " + packageId);
        switch(packageId)
        {
            case 0: 
                AddPinkCoins(20);
                break;
            case 1:
                AddPinkCoins(100);
                break;
            case 2:
                AddPinkCoins(400);
                break;
            case 3:
                AddPinkCoins(1000);
                break;
            default:
                Debug.LogError("Purchaes Package Error: ID does not exist: " + packageId);
                break;
        }
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
