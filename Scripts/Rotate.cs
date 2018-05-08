using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public Vector3 rotateBy;
	public float speed;
	// Update is called once per frame
	void Update () {
		transform.Rotate (rotateBy * speed);
		
	}
}
