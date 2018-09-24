using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCoinTargetController : CoinTargetController
{
    public new void OnHitCoin(GameObject coin)
    {
        animator.SetTrigger("Bump");
        Destroy(coin);
    }

    public new void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "EndscreenCoin")
        {
            OnHitCoin(collider.gameObject);
        }
    }

}
