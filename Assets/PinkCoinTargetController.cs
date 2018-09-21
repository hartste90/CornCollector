using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkCoinTargetController : CoinTargetController
{
    public new void OnHitCoin(GameObject coin)
    {
        //animator.SetTri gger("Bump");
        Destroy(coin);
    }

}
