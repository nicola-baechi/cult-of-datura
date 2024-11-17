using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMoveSpeed = 5f;
    public float verticalMoveSpeed = 1f;
    
    public Rigidbody2D rb;
    
    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        
        moveDirection = new Vector2(moveX, 0).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * horizontalMoveSpeed, verticalMoveSpeed);
    }

    public void ReverseVerticalMoveSpeed()
    {
        Debug.Log("test");
        verticalMoveSpeed *= -1;
    }
}
