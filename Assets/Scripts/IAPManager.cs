using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{

    public EndgameScreenController endgameScreenController;
    public IAPListener purchaseListener;

    public PurchaseConfirmationPanelController purchaseConfirmationPanelController;

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



    public void HandlePurchaseSuccess(Product product)
    {
        string message;
        int coinNum = 0;
        if (product.definition.id == "remove.ads")
        {
            message = "You will no longer see any ads!";
            purchaseConfirmationPanelController.ShowPurchaseSuccess(message);
            GameModel.SetNoAds(true);
        }
        else
        {


            switch (product.definition.id)
            {
                case "pink.glows.20":
                    message = "Obtained 20 Pink Glows!";
                    coinNum = 20;
                    break;
                case "pink.glows.100":
                    message = "Obtained 100 Pink Glows!";
                    coinNum = 100;
                    break;
                case "pink.glows.400":
                    message = "Obtained 400 Pink Glows!";
                    coinNum = 400;
                    break;
                case "pink.glows.1000":
                    message = "Obtained 1000 Pink Glows!";
                    coinNum = 1000;
                    break;
                default:
                    message = "Product: " + product.definition.id + " is not available within the app client";
                    throw new System.Exception(message);

            }
            PurchasePackage(coinNum);
            endgameScreenController.HandleBuyCoinButtonPressed(coinNum);
        }
    }

    public void HandlePurchaseFailure(Product product, PurchaseFailureReason failReason)
    {
        string message = "";
        bool show = false;
        switch (failReason)
        {
            case PurchaseFailureReason.PurchasingUnavailable:
                message = "Purchase Incomplete: Purchasing unavailable on your client.";
                show = true;
                break;
            case PurchaseFailureReason.ExistingPurchasePending:
                message = "Purchase Incomplete: Existing purchase already pending.";
                show = true;
                break;
            case PurchaseFailureReason.ProductUnavailable:
                message = "Purchase Incomplete: Product unavailable.";
                show = true;
                break;
            case PurchaseFailureReason.SignatureInvalid:
                message = "Purchase Incomplete: Your purchase signature is invalid.";
                show = true;
                break;
            case PurchaseFailureReason.UserCancelled:
                message = "Purchase Incomplete: User cancelled purchase.";
                break;
            case PurchaseFailureReason.PaymentDeclined:
                message = "Purchase Incomplete:  Payment was declined.";
                show = true;
                break;
            case PurchaseFailureReason.DuplicateTransaction:
                message = "Purchase Incomplete: Duplicate transaction.";
                show = true;
                break;
            case PurchaseFailureReason.Unknown:
                message = "Purchase Incomplete: Unknown Error.";
                show = true;
                break;
            default:
                message = "Purchase Incomplete: No known error returned.";
                show = true;
                break;
        }
        Debug.Log(message);
        if (show)
        {
            purchaseConfirmationPanelController.ShowPurchaseFailed(message);
        }
    }


}
