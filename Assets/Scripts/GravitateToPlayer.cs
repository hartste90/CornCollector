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

    public Color startColor;
    public Color endColor;
    public bool shouldChangeColor = false;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private float startDistance;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("CoinTarget");
        //GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj)
        {
            playerTransform = playerObj.transform;
        }
        rb = GetComponent<Rigidbody2D>();
        startDistance = GetDistance();
    }
    void Update () 
    {
        if (GameModel.canCollectCoins)
        {
            float distance = GetDistance();
            Vector3 direction = GetDirection();
            AccellerateTowardsTarget(distance, direction);
            if (shouldChangeColor)
            {
                GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(startColor, endColor,  1 - (distance / startDistance));
            }
        }

    }

    private void AccellerateTowardsTarget(float distance, Vector3 direction)
    {
        speed += acceleration;
        rb.AddForce(new Vector2(direction.x, direction.y).normalized * speed * 1/ distance, ForceMode2D.Impulse);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private float GetDistance()
    {
        float distance = 1f;
        if (playerTransform)
        {
            distance = Vector3.Distance(playerTransform.position, transform.position);
        }
        return distance;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(-45, 45, 0);
        if (playerTransform)
        {
            direction = playerTransform.position - transform.position;
        }
        return direction;
    }
}
