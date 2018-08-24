﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateToPlayer : MonoBehaviour {
 
    public float gravityDistance = 10f;
    public float gravityMagnitude = 5f;

    public float speed = 0f;
    public float acceleration = .1f;
    public bool isFollowing = true;
    public float maxSpeed = 10f;

 	private Transform playerTransform;
     private Rigidbody2D rb;

    void Start()
    {
    	playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update () 
    {
        AccellerateTowardsTarget();
    }

    private void AccellerateTowardsTarget()
    {
        Vector3 direction = new Vector3(-45, 45, 0);
        if (playerTransform)
        { 
            direction = playerTransform.position - transform.position; 
        }
        rb.AddForce(new Vector2(direction.x, direction.y).normalized * acceleration, ForceMode2D.Impulse);
        if (rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    

 }