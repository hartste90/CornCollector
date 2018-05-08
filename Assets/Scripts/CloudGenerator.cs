using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour {

	public GameObject cloudPrefab;
	public Transform cloudParent;

	private float nextCloudTime = 0.0f;
	public float period = 2f;

	void Update () {
		if (Time.time > nextCloudTime ) {
			nextCloudTime += period;
			CreateCloud ();
		}
	}

	public void CreateCloud()
	{
		GameObject cloud = Instantiate (cloudPrefab);
		cloud.transform.SetParent (cloudParent);
	}

}
