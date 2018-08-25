using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustController : MonoBehaviour {

    public float shrinkRate;
    private float startingScale;

	// Use this for initialization
	void Start () {
        startingScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x == startingScale / 10)
        {
            Destroy(gameObject);
        }
        transform.localScale *= shrinkRate;
        
	}
}
