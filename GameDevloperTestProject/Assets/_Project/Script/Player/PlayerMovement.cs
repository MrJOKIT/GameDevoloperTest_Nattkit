using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;

    private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimation;
    private float moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (TimeHopManager.Instance.OnTimeSkip)
        {
            SetIdle();
            return;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            playerAnimation.SetBool("IsRun", true);

            Vector3 scale = transform.localScale;
            scale.x = moveInput < 0 ? -1f : 1f;
            transform.localScale = scale;
        }
        else
        {
            playerAnimation.SetBool("IsRun", false);
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //playerAnimation.SetTrigger("Jump"); // for jump animation
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void SetIdle()
    {
        moveInput = 0;
        playerAnimation.SetBool("IsRun",false);
        rb.velocity = Vector2.zero;
    }
}
