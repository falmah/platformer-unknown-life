using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementController : MonoBehaviour
{
    public MonoBehaviour instance { get; set; }
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    public Transform feetPos;

    public float moveSpeed;
    public Func<float> directionCallback;

    private float jumpTimeCounter;
    private bool isGrounded;
    private bool isJumping;

    public float jumpTime;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    public bool jumpable;
    public Func<bool> jumpKeyDownCallback;
    public Func<bool> jumpKeyCallback;
    public Func<bool> jumpKeyUpCallback;

    public Animator animator;

    public MovementController(MonoBehaviour instance)
    {
        this.instance = instance;
    }

    private void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
    }

    private void UpdateMovement()
    {
        float direction = 0;
        if (directionCallback != null)
            direction = directionCallback();

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        if (direction != 0)
        {
            var newScale = new Vector3(direction, 1, 1);
            instance.transform.localScale = newScale;
        }
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    void UpdateJump()
    {
        if (isGrounded && jumpTimeCounter <= 0)
        {
            animator.SetBool("IsJumping", false);
            Debug.Log("State: on ground");
        }

        if (isGrounded && jumpKeyDownCallback() )
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
            Debug.Log("State: jump start");
        }

        if (jumpKeyCallback())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                Debug.Log("State: jump continue");
            }
        }

        if (jumpKeyUpCallback())
        {

            isJumping = false;
            animator.SetBool("IsJumping", false);
            Debug.Log("State: end jump");
        }
    }

    public void MovementFSM()
    {
        IsGrounded();
        if (jumpable)
            UpdateJump();

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        UpdateMovement();
    }



}
