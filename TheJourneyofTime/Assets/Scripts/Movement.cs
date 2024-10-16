using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 3f;  
    public float dashSpeed = 15f;  
    public float dashDuration = 0.2f;
    public float fallThreshold = -10f;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    private bool isFacingRight = true;
    private bool isGrounded = false;  
    private bool canDoubleJump = false;
    private bool canDash = true;
    private bool isDashing;
    private bool isDead = false;
    private Animator animator;

    public float groundCheckRadius = 0.2f;
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

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        FlipSprite();

        if (Input.GetKeyDown(KeyCode.W))
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

        animator.SetBool("isJumping", !isGrounded);
    }

    void HandleJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = true;
            isGrounded = false;
            Debug.Log("Single Jump");
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            Debug.Log("Double Jump");
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

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

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

        if (!wasGrounded && isGrounded)
        {
            canDoubleJump = true;
            canDash = true;
            Debug.Log("Landed and Reset Double Jump and Dash");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true; 
            canDoubleJump = true;
            canDash = true;
            Debug.Log("Landed on Ground - Double Jump and Dash Available");
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
