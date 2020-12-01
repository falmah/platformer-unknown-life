using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float jumpTimeCounter;
    private bool isGrounded;
    private bool isJumping;
    private bool positionChanged;

    public float jumpTime;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float moveSpeed;
    public float jumpForce;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() 
    {
        float direction = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        if (direction != 0)
        {
            var newScale = new Vector3(direction, 1, 1);
            transform.localScale = newScale;
        }
        //cameraMoving();

        animator.SetFloat("Speed", Mathf.Abs(direction));
    }
    
    void cameraMoving()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        var pos = feetPos.position;
        switch (direction) {
            case -1:
                if (!positionChanged)
                    feetPos.position = new Vector3(pos.x + 20, pos.y, pos.z);
                break;
            case 0:
                feetPos.position = new Vector3(pos.x - 20, pos.y, pos.z);
                break;
            case 1:
                if(!positionChanged)
                    feetPos.position = new Vector3(pos.x - 20, pos.y, pos.z);
                break;

        }     
    }
    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded && jumpTimeCounter<=0)
        {
            animator.SetBool("IsJumping", false);
        }
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }
}

