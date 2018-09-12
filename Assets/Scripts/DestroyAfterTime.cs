using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	public float timeUntilDestroy;
    public ExplosionPuffController explosionPuffController;
	// Use this for initialization
	void Start () {
        explosionPuffController = GetComponent<ExplosionPuffController>();
	}
	
	// Update is called once per frame
	void Update () {
	        timeUntilDestroy -= Time.deltaTime;
	        if (timeUntilDestroy <= 0)
	        {
	                DestroySelf();
	        }
		
	}

	public void DestroySelf()
	{
        explosionPuffController.gameController.explosionPuffList.Remove(gameObject);
		Destroy (gameObject);
	}
}
