using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private InputManager inputManager;

    private Vector2 movement;
    private bool jumpIsPressedDown;

    private bool isGrounded = false;
    private bool isJumping = false;

    private void Awake()
    {
        inputManager = new InputManager();

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = inputManager.Player.Move.ReadValue<Vector2>();
        jumpIsPressedDown = Mathf.Abs(inputManager.Player.Jump.ReadValue<float>()) > 0;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("Grounded: " + isGrounded);
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }


    private void OnEnable()
    {
        inputManager.Player.Enable();
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
    }

    private void Move()
    {
        //Debug.Log(movement);
        //Debug.Log((int)(movement * Vector2.right).normalized.x);

        int normalizedMoveX = (int)(movement * Vector2.right).normalized.x;
        rigidBody.velocity = new Vector2(normalizedMoveX * moveSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
    }

    private void Jump()
    {
        //Debug.Log(jumpIsPressedDown);
        if (jumpIsPressedDown && isGrounded && rigidBody.velocity.y == 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime);
        }
    }
}
