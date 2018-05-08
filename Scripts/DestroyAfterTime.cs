using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	public float timeUntilDestroy;
	// Use this for initialization
	void Start () {
		
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
		Destroy (gameObject);
	}
}
