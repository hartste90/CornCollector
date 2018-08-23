using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateToPlayer : MonoBehaviour {
 
    public float gravityDistance = 10f;
    public float gravityMagnitude = 5f;

 	private Transform playerTransform;

    void Start()
    {
    	playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update () 
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        // Debug.Log(distance);
        if (distance < gravityDistance)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, gravityMagnitude * Time.deltaTime);
        }
    }

 }
