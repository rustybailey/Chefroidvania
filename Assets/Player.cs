using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private InputManager inputManager;

    private Vector2 movement;
    private bool jumpIsPressedDown;

    private bool isGrounded = false;
    private bool isJumping = false;

    private bool started = false;

    private void Awake()
    {
        inputManager = new InputManager();

    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement = inputManager.Player.Move.ReadValue<Vector2>();
        jumpIsPressedDown = Mathf.Abs(inputManager.Player.Jump.ReadValue<float>()) > 0;

        isGrounded = Physics2D.OverlapBox(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y - boxCollider.bounds.size.y / 2), new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y * .2f), 0f, groundLayer);
        Debug.Log("Grounded: " + isGrounded);
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        if (!started) { return; }
        Gizmos.color = Color.red;
        //Draw a cube where the OverlapBox is
        Gizmos.DrawWireCube(new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.center.y - boxCollider.bounds.size.y / 2), new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y * .2f));
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
        Debug.Log(movement);
        if (movement.x != 0)
        {
            float moveX = movement.x > 0 ? moveSpeed : -moveSpeed;
            rigidBody.velocity = new Vector2(moveX * Time.fixedDeltaTime, rigidBody.velocity.y);
        }
    }

    private void Jump()
    {
        Debug.Log(jumpIsPressedDown);
        if (jumpIsPressedDown && isGrounded)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
    }
}
