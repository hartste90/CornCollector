﻿using UnityEngine;

public class CoinTargetController : MonoBehaviour {


    public GameController gameController;
    public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnHitCoin(GameObject coin)
    {
        animator.SetTrigger("Bump");
        gameController.CheckCoinsCollected(coin);
        Destroy(coin);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Coin")
        {
            OnHitCoin(collider.gameObject);
        }
    }
}
