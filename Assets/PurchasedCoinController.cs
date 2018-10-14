using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasedCoinController : MonoBehaviour {

    public GameObject coinImage;

    public GameObject uiRollupCoinPrefab;
    public Transform coinStartTransform;
    public Transform coinEndTransform;

    private Animator animator;
    private int numCoinsToCreate = 100;



	// Use this for initialization
	void Awake () 
    {
        animator = GetComponent<Animator>();
        coinStartTransform = transform;
        animator.SetTrigger("Explode");
	}

    //send the # coins and where you want them to go when you create this gameObject
    public void Populate(Transform endTransform, int numCoins = 100)
    {
        numCoinsToCreate = numCoins;
        coinEndTransform = endTransform;
    }

    public void HandleCoinExploded()
    {
        Debug.Log("Exploded: " + numCoinsToCreate);
        for (int i = 0; i < numCoinsToCreate; i++)
        {
            //create all the coins we need so they will gravitate to target
            GameObject pinkCoin = Instantiate(uiRollupCoinPrefab, coinStartTransform);
            pinkCoin.GetComponent<GravitateToTarget>().SetTarget(coinEndTransform);
            pinkCoin.transform.localPosition = Vector3.zero;
        }
        Debug.Break();
        //destroy main coin
        Destroy(coinImage);
    }
}
