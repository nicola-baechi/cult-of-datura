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
    private bool isDashing = false;
    
    

    [SerializeField]
    private TrailRenderer tr;
    
    public Rigidbody2D rb;
    
    private Vector2 moveDirection;

    private void Start()
    {
        tr = GetComponent<TrailRenderer>();
        
        if (tr != null)
            tr.enabled = false;
    }

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
        if (isDashing)
        {
            return;
        }
        
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
        isDashing = true;
        float originalSpeed = horizontalMoveSpeed;

        //Enable Trail renderer
        if (tr != null)
        {
            tr.enabled = true;
        }
        
        //Increase Speed for dashing
        horizontalMoveSpeed = dashSpeed * direction;
        
        yield return new WaitForSeconds(dashDuration);
        
        //Speed reset
        horizontalMoveSpeed = Mathf.Abs(originalSpeed) * (direction < 0 ? -1 : 1);

        //Disable Trail renderer
        if (tr != null)
        {
            tr.enabled = false;
        }

        isDashing = false;

    }
}

