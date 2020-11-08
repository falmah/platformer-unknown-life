using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int starsCount = 0;
    public Text starsText;


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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            starsCount++;
            starsText.text = starsCount.ToString();
        }
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
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.W)) 
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }
}

