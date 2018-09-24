using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitateToTarget : MonoBehaviour {
 
    public float gravityDistance = 10f;
    public float gravityMagnitude = 5f;

    public float speed = 1f;
    public float acceleration = .1f;
    public bool isFollowing = true;
    public float maxSpeed = 10f;

    public Color startColor;
    public Color endColor;
    public bool shouldChangeColor = false;

    public Transform targetTransform;
    private Rigidbody2D rb;
    private float startDistance;
    private bool shouldGravitate;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shouldGravitate = true;
        startDistance = GetDistance();
    }
    void Update () 
    {
        if (shouldGravitate == true)
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

    public void SetTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public void SetShouldGravitate(bool shouldGravitateSet)
    {
        this.shouldGravitate = shouldGravitateSet;
        if (shouldGravitateSet == false)
        {
            this.speed = 1f;
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
        if (targetTransform)
        {
            distance = Vector3.Distance(targetTransform.position, transform.position);
        }
        return distance;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(-45, 45, 0);
        if (targetTransform)
        {
            direction = targetTransform.position - transform.position;
        }
        return direction;
    }
}
