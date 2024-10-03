using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 10f;  
    private bool isGrounded;  
    private bool canDoubleJump;  
    
    public float groundCheckRadius = 0.2f;  
    public LayerMask whatIsGround;  
    public Transform groundCheck; 
    private Rigidbody2D rb;
     
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // walking
        float moveInput = Input.GetAxis("Horizontal");  
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);  

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;  
        }

        // jump 
        if (Input.GetButtonDown("Jump"))
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // normal jump
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // double jump
                canDoubleJump = false;
            }
        }
    }
}
