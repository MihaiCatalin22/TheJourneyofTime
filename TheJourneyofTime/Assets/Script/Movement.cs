using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    //public float moveInput;  
    public float jumpForce = 10f;  
    public float dashSpeed = 15f;  
    public float dashDuration = 0.2f;
    public float fallThreshold = -10f; // Threshold to trigger reset
    public Vector3 startPosition; // Start position for reset

    public float ledgeCheckDistance = 0.5f; // Distance for ledge check raycast
    public Transform groundCheck; // Position from which to check if grounded
    public Transform ledgeCheck; // Position from which to check for ledge above
    public LayerMask whatIsGround; // Layer defining ground surfaces
    
    private bool isFacingRight = false;
    private bool isGrounded = false;  
    private bool canDoubleJump = false;  
    private bool isDashing;  
    private bool isHanging;  
    private float hangingTimer;
    private float maxHangingTime = 1.0f; // Max time allowed to hang
    private Animator animator;

    public float groundCheckRadius = 0.2f; // Radius for ground check
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Set initial start position
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        FlipSprite();

        if (isDashing) return; // Skip other controls while dashing

        float moveInput = Input.GetAxis("Horizontal");  
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);  

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        if (isGrounded)
        {
            canDoubleJump = true;
            // Reset hanging state when grounded
            ExitHanging();
        }

        // Jump and Double Jump
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }

        }

        // Dash when moving horizontally
        if (Input.GetKeyDown(KeyCode.LeftShift) && moveInput != 0)
        {
            StartCoroutine(Dash(moveInput));
        }

        // Check for ledge if falling and not grounded
        if (!isGrounded && rb.velocity.y < 0 && !isHanging)
        {
            CheckForLedge();
        }

        // Automatically exit hanging after max hanging time
        if (isHanging)
        {
            hangingTimer += Time.deltaTime;
            if (hangingTimer >= maxHangingTime)
            {
                ExitHanging();
            }
        }

        // Climb up when hanging
        if (isHanging && Input.GetKeyDown(KeyCode.W))
        {
            ClimbUp();
        }

        // Reset position if fallen below threshold
        if (transform.position.y < fallThreshold)
        {
            ResetPosition();
        }
        
        animator.SetBool("isJumping", !isGrounded);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        canDoubleJump = true;
        isGrounded = false;
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0; // Temporarily disable gravity during dash

        rb.velocity = new Vector2(direction * dashSpeed, 0);
        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity; // Restore gravity after dash
        isDashing = false;
    }

    void CheckForLedge()
    {
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, ledgeCheckDistance, whatIsGround);
        RaycastHit2D ledgeHit = Physics2D.Raycast(ledgeCheck.position, Vector2.up, ledgeCheckDistance, whatIsGround);

        if (wallHit.collider != null && ledgeHit.collider == null) // If there's a wall but no ledge above
        {
            rb.velocity = Vector2.zero; // Stop vertical movement
            rb.isKinematic = true; // Temporarily disable physics to hang
            isHanging = true;
            hangingTimer = 0f; // Reset hanging timer
        }
    }

    void ClimbUp()
    {
        float ledgeOffsetX = 0.5f; // Offset to adjust climbing distance on X
        float ledgeOffsetY = 1.2f; // Offset to adjust climbing height on Y

        Vector3 climbPosition = new Vector3(transform.position.x + (ledgeOffsetX * transform.localScale.x), 
                                            transform.position.y + ledgeOffsetY, 
                                            transform.position.z);

        transform.position = climbPosition; // Move to new position at top of ledge
        ExitHanging(); // Exit hanging state after climbing
    }

    void ExitHanging()
    {
        rb.isKinematic = false; // Re-enable physics
        isHanging = false; 
        hangingTimer = 0f; // Reset the hanging timer
    }

    void ResetPosition()
    {
        transform.position = startPosition; // Move back to start
        rb.velocity = Vector2.zero; // Stop any movement
        rb.isKinematic = false; // Make sure physics is enabled after reset
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null || ledgeCheck == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Visualize ground check radius
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * ledgeCheckDistance); // Visualize ledge check
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.up * ledgeCheckDistance); // Visualize ledge check above
      
    }


    void FlipSprite(){
        if (isFacingRight && Input.GetAxis("Horizontal") < 0f || !isFacingRight && Input.GetAxis("Horizontal") > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            canDoubleJump = false; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false; 
        }
    }
    
}
