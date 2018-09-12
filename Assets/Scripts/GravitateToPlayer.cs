using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateToPlayer : MonoBehaviour {
 
    public float gravityDistance = 10f;
    public float gravityMagnitude = 5f;

    public float speed = 1f;
    public float acceleration = .1f;
    public bool isFollowing = true;
    public float maxSpeed = 10f;

 	private Transform playerTransform;
     private Rigidbody2D rb;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("CoinTarget");
        //GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)
        {
            playerTransform = playerObj.transform;
        }
        rb = GetComponent<Rigidbody2D>();
    }
    void Update () 
    {
        if (GameModel.canCollectCoins)
        {
            AccellerateTowardsTarget();
        }

    }

    private void AccellerateTowardsTarget()
    {
        Vector3 direction = new Vector3(-45, 45, 0);
        float distance = 1;
        if (playerTransform)
        { 
            direction = playerTransform.position - transform.position;
            distance = Vector3.Distance(playerTransform.position, transform.position);
        }
        speed += acceleration;
        rb.AddForce(new Vector2(direction.x, direction.y).normalized * speed * 1/ distance, ForceMode2D.Impulse);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
 }
