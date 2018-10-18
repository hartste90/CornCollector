using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCoinTargetController : CoinTargetController
{
    public EndgameScreenController endScreenController;
    public RollupController rollupController;

    public List<GameObject> coinList;

    private void Awake()
    {
        coinList = new List<GameObject>();
    }

    public new void OnHitCoin(GameObject coin)
    {
        animator.SetTrigger("Bump");
        Destroy(coin);
    }

    private void OnCollectPurchaseCoin(GameObject coin)
    {
        rollupController.OnCollectPurchaseCoin();

        animator.SetTrigger("Bump");
        Destroy(coin);
        coinList.Remove(coin);
        if (coinList.Count == 0)
        {
            endScreenController.OnPurchaseAnimationOver();
        }

    
    }

    public new void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "EndscreenCoin")
        {
            OnHitCoin(collider.gameObject);
        }
        else if (collider.gameObject.tag == "PurchaseCoin")
        {
            OnCollectPurchaseCoin(collider.gameObject);
        }
    }

}
