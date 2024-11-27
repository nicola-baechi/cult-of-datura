using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Vertical = Animator.StringToHash("verticalInput");

    public float horizontalMoveSpeed = 5f;
    public float verticalMoveSpeed = 3f;

    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float doubleTapTime = 0.2f;

    public Animator animator;

    private float lastTapTimeA;
    private float lastTapTimeD;
    private bool isDashing;

    private bool controllsEnabled = true;

    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D rb;

    private Vector2 moveDirection;
    private void Start()
    {
        // NOTE: needs to be here because onEnable would be too early since EventManager is not yet initialized
        EventManager.Instance.onPlayerCollectHealItem.AddListener(HandleHealthyState);
        EventManager.Instance.onPlayerHit.AddListener(HandleHypnotizedState);
        EventManager.Instance.onPlayerDie.AddListener(HandleFullyHypnotizedState);
    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayerCollectHealItem.RemoveListener(HandleHealthyState);
        EventManager.Instance.onPlayerHit.RemoveListener(HandleHypnotizedState);
        EventManager.Instance.onPlayerDie.RemoveListener(HandleFullyHypnotizedState);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        tr = GetComponent<TrailRenderer>();
        if (tr != null)
            tr.enabled = false;
    }


private void Update()
    {
        animator.SetFloat(Vertical, verticalMoveSpeed);
        if(!controllsEnabled) return;
        
        float moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector2(moveX, 0).normalized;
        
        CheckForDash();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * horizontalMoveSpeed, verticalMoveSpeed);
    }

    public void HandleHypnotizedState()
    {
        ReverseVerticalMoveSpeed();
    }

    public void HandleHealthyState()
    {
        ReverseVerticalMoveSpeed();
    }
    
    public void HandleFullyHypnotizedState()
    {
        controllsEnabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        verticalMoveSpeed = -1;
        moveDirection = Vector2.zero;
    }

    private void ReverseVerticalMoveSpeed()
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

