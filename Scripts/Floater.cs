using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {

	public float verticalSpeed;
	public float amplitude;
	public float initialYValue;

	public Vector3 tempPosition;

	public void Start()
	{
		tempPosition = transform.position;
		initialYValue = tempPosition.y;
	}

	void FixedUpdate()
	{
		tempPosition.y = (Mathf.Sin (Time.realtimeSinceStartup * verticalSpeed) * amplitude) + initialYValue;
		transform.position = tempPosition;
	}
}
