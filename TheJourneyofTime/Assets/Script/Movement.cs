using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;  
    public float dashSpeed = 15f;  
    public float dashDuration = 0.2f;
    public float fallThreshold = -10f;
    public Vector3 startPosition;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    private bool isFacingRight = true;
    private bool isGrounded = false;  
    private bool canDoubleJump = false;
    private bool isDashing;
    private bool isDead = false;
    private Animator animator;

    public float groundCheckRadius = 0.2f;
    private Rigidbody2D rb;

    public GameObject deathText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
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

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        FlipSprite();

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.W))
        {
            HandleJump();
        }

        // Handle dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(moveInput) > 0)
        {
            StartCoroutine(Dash(Mathf.Sign(moveInput)));
        }

        // Check if player fell below threshold
        if (transform.position.y < fallThreshold)
        {
            StartCoroutine(Respawn());
        }

        animator.SetBool("isJumping", !isGrounded);
    }

    void HandleJump()
    {
        if (isGrounded)
        {
            // Single jump from the ground
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true; // Enable double jump after the first jump
            isGrounded = false; // Set to false since we're now airborne
            Debug.Log("Single Jump"); // Log for single jump
        }
        else if (canDoubleJump)
        {
            // Double jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false; // Disable further jumps after the double jump
            Debug.Log("Double Jump"); // Log for double jump
        }
    }

    IEnumerator Dash(float direction)
    {
        if (isDashing) yield break;

        isDashing = true;
        rb.gravityScale = 0;

        rb.velocity = new Vector2(direction * dashSpeed, 0);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = 1;
        isDashing = false;
    }

    public IEnumerator Respawn()
    {
        if (deathText != null)
            deathText.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        transform.position = startPosition;
        yield return new WaitForFixedUpdate();

        rb.isKinematic = false;
        isDead = false;

        if (deathText != null)
            deathText.SetActive(false);
        
        CheckGroundStatus(); // Ensure ground status is updated after respawn
    }

    void CheckGroundStatus()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Reset double jump when player lands
        if (!wasGrounded && isGrounded)
        {
            canDoubleJump = true;
            Debug.Log("Landed and Reset Double Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true; 
            canDoubleJump = true; 
            Debug.Log("Landed on Ground - Double Jump Available");
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
}
