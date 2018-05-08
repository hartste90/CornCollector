using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionController : ExplosionController {

	// Use this for initialization
	void Start () {
		//instantiate explosionPuffs
		explosionPuffObjectList = new GameObject[4];
		for(int i = 0; i < 4; i++)
		{
		        GameObject explosionPuffObject = Instantiate (explosionPuffPrefab, transform, true);
		        explosionPuffObject.transform.localPosition = transform.localPosition;
		        explosionPuffObject.transform.localScale = Vector3.one;
		        explosionPuffObjectList[i] = explosionPuffObject;
		}
		explosionPuffObjectList[0].GetComponent<Rigidbody2D>().AddForce(Vector2.right * explosionStrength, ForceMode2D.Force);
		explosionPuffObjectList[1].GetComponent<Rigidbody2D>().AddForce(Vector2.up * explosionStrength, ForceMode2D.Force);
		explosionPuffObjectList[2].GetComponent<Rigidbody2D>().AddForce(Vector2.left * explosionStrength, ForceMode2D.Force);
		explosionPuffObjectList[3].GetComponent<Rigidbody2D>().AddForce(Vector2.down * explosionStrength, ForceMode2D.Force);

	}
}
