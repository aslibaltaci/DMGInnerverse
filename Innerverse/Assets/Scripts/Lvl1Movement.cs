using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1Movement : MonoBehaviour
{
    public float moveForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * moveForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveForce);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * moveForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * moveForce);
        }
    }
}
