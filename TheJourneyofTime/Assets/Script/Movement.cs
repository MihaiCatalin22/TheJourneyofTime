using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    //public float moveInput;  
    public float jumpForce = 10f;  
    public bool isFacingRight = false;
    private bool isGrounded = false;  
    private bool canDoubleJump = false;  
    
    
    //public float groundCheckRadius = 0.2f;  
    public LayerMask whatIsGround;  
    //public Transform groundCheck; 
    private Rigidbody2D rb;
    private Animator animator;
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        FlipSprite();

        //@cata change this
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = true;
                isGrounded = false;
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false;
            }
        }
        
        animator.SetBool("isJumping", !isGrounded);
        

        
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
    
    // // walking
    // float moveInput = Input.GetAxis("Horizontal");  
    // rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);  

    // isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    // if (isGrounded)
    // {
    //     canDoubleJump = true;  
    // }

    // // jump 
    // if (Input.GetButtonDown("Jump"))
    // {
    //     //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //     if (isGrounded)
    //     {
    //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // normal jump
    //     }
    //     else if (canDoubleJump)
    //     {
    //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // double jump
    //         canDoubleJump = false;
    //     }
    // }
    //}
}
