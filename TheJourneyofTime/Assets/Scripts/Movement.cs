using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float jumpForce = 10f;  
    public float dashSpeed = 30f;  
    public float dashDuration = 0.2f;
    public float fallThreshold = -10f;

    public ParticleSystem dust;
    public Transform groundCheck;
    public Transform climbCheck;
    public LayerMask whatIsGround;
    public LayerMask climbableLayer;
    
    public float coyoteTimeDuration = 0.2f;
    private float coyoteTimeCounter; 
    private bool canClimb = false;
    private bool isFacingRight = true;
    private bool isGrounded = false;  
    private bool canDoubleJump = false;
    public bool canDash = true;
    private bool isDashing;
    private bool isDead = false;
    private Animator animator;

    public float groundCheckRadius = 0.2f;
    public float climbSpeed = 8f; 
    private Rigidbody2D rb;

    public DashingSound dashingSound;
    public JumpingSound jumpingSound;
    public LandingSound landingSound;
    public DamageSound damageSound;
    public VineClimbingSound vineClimbingSound;
    private bool slipperyMode = false;
    private float slipperyFactor = 1f;
    private float slidingMomentum = 0f;
    public float slideDecayRate = 0.98f;

    public GameObject deathText;

    public bool isRunning { get; private set; }
    public bool isClimbing { get; private set; }
    public bool JumpedThisFrame { get; private set; }
    public bool LandedThisFrame { get; private set; }
    public bool DashedThisFrame { get; private set; }

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

        HandleClimbing();

        if (!isClimbing)
        {
            HandleMovement();
            FlipSprite();

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                StartCoroutine(Dash(Mathf.Sign(Input.GetAxis("Horizontal"))));
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

        CheckGroundStatus();

        JumpedThisFrame = false;
        LandedThisFrame = false;
        DashedThisFrame = false;
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        isRunning = Mathf.Abs(moveInput) > 0.1f;
        Debug.Log("isRunning: " + isRunning);

        if (slipperyMode)
        {
            if (Mathf.Abs(moveInput) > 0.1f)
            {
                slidingMomentum = moveInput * moveSpeed * slipperyFactor;
                rb.velocity = new Vector2(slidingMomentum, rb.velocity.y);
            }
            else
            {
                slidingMomentum *= slideDecayRate;
                rb.velocity = new Vector2(slidingMomentum, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    public void SetSlippery(bool isSlippery, float factor)
    {
        slipperyMode = isSlippery;
        slipperyFactor = factor;
    }

   void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Climbable"))
    {
        canClimb = true;
    }
}

void OnTriggerExit2D(Collider2D other)
{
    if (other.gameObject.layer == LayerMask.NameToLayer("Climbable"))
    {
        canClimb = false;
        EndClimbing();
    }
}

void HandleClimbing()
{
    if (canClimb && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && !isClimbing)
    {
        isClimbing = true;
        rb.velocity = Vector2.zero;
        canDoubleJump = true;
        canDash = true;

        if (vineClimbingSound != null)
        {
            vineClimbingSound.PlayVineClimbSound();
        }
    }

    if (isClimbing)
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * climbSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndClimbing();
        }
    }
}

void EndClimbing()
{
    isClimbing = false;

    if (vineClimbingSound != null)
    {
        vineClimbingSound.StopVineClimbSound();
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
            JumpedThisFrame = true;
            Debug.Log("Player Jumped");
            
            if (jumpingSound != null)
            {
                jumpingSound.PlayJumpSound();
            }
        }
        else if (canDoubleJump)
        {
            CreateDust();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canDoubleJump = false;
            JumpedThisFrame = true;
            Debug.Log("Player Double Jumped");
            
            if (jumpingSound != null)
            {
                jumpingSound.PlayJumpSound();
            }
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
        DashedThisFrame = true;
        
        if (dashingSound != null)
        {
            dashingSound.PlayDashSound();
            CreateDust();
        }

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    public void Die()
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
    Collider2D groundCollider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    isGrounded = groundCollider != null;


    if (!wasGrounded && isGrounded)
    {
        canDoubleJump = true;
        canDash = true;
        LandedThisFrame = true;

        if (landingSound != null)
        {
            landingSound.PlayLandingSound();
        }
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

    void CreateDust(){
        dust.Play();
    }
}
