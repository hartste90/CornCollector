using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPuffController : MonoBehaviour {


    public GameController gameController;
    private bool isBeingDestroyed = false;

    private void Update()
    {
        if (isBeingDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public void DestroySelf(bool immediate)
	{
        gameController.explosionPuffList.Remove(gameObject);
        if (immediate)
        {
            Destroy(gameObject);

        }
        else
        {
            isBeingDestroyed = true;
        }

	}
}
