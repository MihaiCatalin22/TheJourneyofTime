using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 3f;  
    public float dashSpeed = 15f;  
    public float dashDuration = 0.2f;
    public float fallThreshold = -10f;

    public Transform groundCheck;
    //public Transform climbCheck;
    public LayerMask whatIsGround;
    //public LayerMask climbableLayer;

    public float coyoteTimeDuration = 0.2f;
    private float coyoteTimeCounter; 

    private bool isFacingRight = true;
    private bool isGrounded = false;  
    private bool canDoubleJump = false;
    public bool canDash = true;
    private bool isDashing;
    //private bool isClimbing = false;
    private bool isDead = false;
    private Animator animator;

    public float groundCheckRadius = 0.2f;
    //public float climbSpeed = 3f; 
    private Rigidbody2D rb;

    public GameObject deathText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (deathText != null)
            deathText.SetActive(false);
    }

    void FixedUpdate()
    {
        if (isDead) return;

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void Update()
    {
        if (isDead || isDashing) return;

        //HandleClimbing();

        //if (!isClimbing)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            FlipSprite();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && Mathf.Abs(moveInput) > 0)
            {
                StartCoroutine(Dash(Mathf.Sign(moveInput)));
            }

            if (transform.position.y < fallThreshold)
            {
                Die();
            }
        }

        animator.SetBool("isJumping", !isGrounded);

        if (!isGrounded)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    void HandleJump()
    {
        if (isGrounded || coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true;
            isGrounded = false;
            coyoteTimeCounter = 0f;
            Debug.Log("Jumping. Double jump available.");
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            Debug.Log("Double jump.");
        }
    }

    IEnumerator Dash(float direction)
    {
        if (isDashing) yield break;

        isDashing = true;
        canDash = false;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        rb.velocity = new Vector2(direction * dashSpeed, 0);
        Debug.Log("Dashing.");

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

   // void HandleClimbing()
   // {
      //  bool isTouchingClimbable = Physics2D.OverlapCircle(climbCheck.position, groundCheckRadius, climbableLayer);
      //  Debug.Log($"Climbable Check: {isTouchingClimbable}");
//
      //  if (isTouchingClimbable && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !isClimbing)
//{
      //      // Start climbing
      //      isClimbing = true;
      //      rb.gravityScale = 0;
      //      rb.velocity = Vector2.zero;
      //      Debug.Log("Started climbing.");
      //  }

      //  if (isClimbing)
      //  {
      //      float verticalInput = Input.GetAxis("Vertical");
      //      float horizontalInput = Input.GetAxis("Horizontal");
//
      //      rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * climbSpeed);
      //      Debug.Log($"Climbing... Vertical: {verticalInput}, Horizontal: {horizontalInput}");
//
      //      if (!isTouchingClimbable || Input.GetKeyDown(KeyCode.Space))
     //       {
     //           isClimbing = false;
    //            rb.gravityScale = 1;
    //            Debug.Log("Stopped climbing.");
    //        }
    //    }
    //}

    void Die()
    {
        if (deathText != null)
        {
            deathText.SetActive(true);
        }

        isDead = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTimeDuration;
        }

        if (!wasGrounded && isGrounded)
        {
            canDoubleJump = true;
            canDash = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true; 
            canDoubleJump = true;
            canDash = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false; 
        }
    }

    void FlipSprite()
    {
        if (isFacingRight && Input.GetAxis("Horizontal") < 0f || !isFacingRight && Input.GetAxis("Horizontal") > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void SetDead(bool dead)
    {
        isDead = dead;
    }

    public void ResetMovementState()
    {
        isDead = false;
        canDash = true;
        canDoubleJump = true;
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }
}
