using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

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
        moveInput = Input.GetAxisRaw("Horizontal");
        
        if (moveInput != 0)
        {
            playerAnimation.SetBool("IsRun",true);
            playerSprite.flipX = moveInput < 0;
        }
        else
        {
            playerAnimation.SetBool("IsRun",false);
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
