using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public float maxTrajectory = 10000f;

	void Start()
	{
			//give random small trajectory
			Vector2 randomDirection = new Vector2(Random.Range(-maxTrajectory, maxTrajectory), Random.Range(-maxTrajectory, maxTrajectory));
			GetComponent<Rigidbody2D>().AddForce(randomDirection, ForceMode2D.Impulse);
	}
}
