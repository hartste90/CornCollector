using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

	public Vector3 initialPosition;
	public float cloudSpeed;


	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3 (10, transform.localPosition.y, 0);
		initialPosition = transform.localPosition;
		rb = GetComponent<Rigidbody2D> ();

		rb.velocity = new Vector3 (-cloudSpeed, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < - 10) 
		{
			Destroy (gameObject);
		}
	}

	public void DestroySelf()
	{
	        Destroy(gameObject);
	}
}
