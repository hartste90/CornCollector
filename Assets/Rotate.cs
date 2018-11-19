using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float multiplier = .1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rotAmt = Mathf.Sin(Time.time);

        transform.Rotate(Vector3.forward, rotAmt * multiplier);
	}
}
