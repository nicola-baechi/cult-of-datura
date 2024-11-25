using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("verticalInput");
    public float horizontalMoveSpeed = 5f;
    public float verticalMoveSpeed = 1f;
    
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float doubleTapTime = 0.2f;

    public Animator animator;

    private bool isFullyHypnotized;
    private float lastTapTimeA = 0f;
    private float lastTapTimeD = 0f;
    private bool isDashing = false;
    
    [SerializeField]
    private TrailRenderer tr;
    
    public Rigidbody2D rb;
    
    private Vector2 moveDirection;
    
    [SerializeField]
    private AudioSource hypnotizedSound;

    private void Start()
    {
        tr = GetComponent<TrailRenderer>();
        
        if (tr != null)
            tr.enabled = false;
    }
    
    private void Update()
    {
        animator.SetFloat(Vertical, verticalMoveSpeed);
        if (isFullyHypnotized) return;
        
        float moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector2(moveX, 0).normalized;
        
        CheckForDash();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * horizontalMoveSpeed, verticalMoveSpeed);
        
        if (rb.velocity.y < 0)
        {
            PlayHyponotizedSound();
        }
        else
        {
            StopHyponotizedSound();
        }
    }

    public void ReverseVerticalMoveSpeed()
    {
        verticalMoveSpeed *= -1;
    }
    
    public void SetFullyHypnotized()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        isFullyHypnotized = true;
        moveDirection = new Vector2(0, moveDirection.y).normalized;
        ReverseVerticalMoveSpeed();
        PlayHyponotizedSound();
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
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
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
        boxCollider2D.enabled = true;
    }
    
    private void PlayHyponotizedSound()
    {
        if (hypnotizedSound != null && !hypnotizedSound.isPlaying)
        {
            hypnotizedSound.Play();
        }
    }
    
    private void StopHyponotizedSound()
    {
        if (hypnotizedSound != null && hypnotizedSound.isPlaying)
        {
            hypnotizedSound.Stop();
        }
    }
}

