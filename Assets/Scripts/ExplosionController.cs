﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

	public GameObject explosionPuffPrefab;
	public GameObject[] explosionPuffObjectList;
    public GameController gameController;

	public float explosionStrength;
	// Use this for initialization
	void Start () {
        OnStart();
	}

    public void OnStart()
    {
        //instantiate explosionPuffs
        explosionPuffObjectList = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            GameObject explosionPuffObject = Instantiate(explosionPuffPrefab, transform.parent, true);
            ExplosionPuffController puffCtr = explosionPuffObject.GetComponent<ExplosionPuffController>();
            puffCtr.gameController = gameController;
            gameController.explosionPuffList.Add(explosionPuffObject);
            explosionPuffObject.transform.localPosition = transform.localPosition;
            //explosionPuffObject.transform.localScale = Vector3.one;
            explosionPuffObjectList[i] = explosionPuffObject;
            explosionPuffObjectList[i].GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1f, 1f), ForceMode2D.Impulse);

        }

        explosionPuffObjectList[0].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.right).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[0].GetComponent<Transform>().Rotate(new Vector3(0, 0, -90));
        explosionPuffObjectList[1].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.up).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[2].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.left).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[2].GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        explosionPuffObjectList[3].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.down).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[3].GetComponent<Transform>().Rotate(new Vector3(0, 0, 180));

        Destroy(gameObject);
    }

}
