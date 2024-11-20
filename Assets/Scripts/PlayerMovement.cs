using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontalMoveSpeed = 5f;
    public float verticalMoveSpeed = 1f;
    
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float doubleTapTime = 0.2f;

    private float lastTapTimeA = 0f;
    private float lastTapTimeD = 0f;
    
    public Rigidbody2D rb;
    
    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector2(moveX, 0).normalized;
        
        CheckForDash();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * horizontalMoveSpeed, verticalMoveSpeed);
    }

    public void ReverseVerticalMoveSpeed()
    {
        verticalMoveSpeed *= -1;
    }

    private void CheckForDash()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTimeA < doubleTapTime)
            {
                StartCoroutine(Dash(1));
            }
            lastTapTimeA = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTimeD < doubleTapTime)
            {
                StartCoroutine(Dash(1));
            }
            lastTapTimeD = Time.time;
        }
    }

    private IEnumerator Dash(int direction)
    {
        float originalSpeed = horizontalMoveSpeed;
        horizontalMoveSpeed = dashSpeed * direction;
        yield return new WaitForSeconds(dashDuration);
        horizontalMoveSpeed = Mathf.Abs(originalSpeed);
    }
    
    
}
