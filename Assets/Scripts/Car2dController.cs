﻿using UnityEngine;

public class Car2dController : MonoBehaviour
{
    float speedForce = 3f;
    float driftFactor = 0.8f;

    Quaternion forward = Quaternion.Euler(0, 0, 0);
    Quaternion right = Quaternion.Euler(0, 0, -90);
    Quaternion left = Quaternion.Euler(0, 0, 90);

    string dir;
    void FixedUpdate()
    {
        if (GameManager.Instance.isGameStarted)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = ForwardVelocity() + RightVelocity() * driftFactor;
            rb.AddForce(transform.up * speedForce);

            if (Input.GetMouseButton(0))
            {
                Turn();
            }
        }
    }
    Vector2 ForwardVelocity()
    {
        return transform.up * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.up);
    }
    Vector2 RightVelocity()
    {
        return transform.right * Vector2.Dot(GetComponent<Rigidbody2D>().velocity, transform.right);
    }

    private void Turn()
    {
        if (Vector3.Angle(Vector3.up, transform.up) < 30)
        {
            if (!(dir == "forward"))
            {
                dir = "forward";
                transform.rotation = Quaternion.Slerp(transform.rotation, forward, 0.19f);
                Quaternion.Slerp(transform.rotation, forward, 1f);
            }
        }
        else if (Vector3.Angle(Vector3.right, transform.up) < 30)
        {
            if (!(dir == "right"))
            {
                dir = "right";
                transform.rotation = Quaternion.Slerp(transform.rotation, right, 0.19f);
            }
            dir = "right";
        }
        else if (Vector3.Angle(Vector3.left, transform.up) < 30)
        {
            if (!(dir == "left"))
            {
                dir = "left";
                transform.rotation = Quaternion.Slerp(transform.rotation, left, 0.19f);
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("çarptı" + collision);
        if (collision.gameObject.tag == "Road")
        {
            GameManager.Instance.RestartGame();
        }
    }
}
