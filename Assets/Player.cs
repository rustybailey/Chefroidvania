using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float jumpSpeed = 8.0f;

    private Rigidbody2D rigidBody;
    private InputManager inputManager;

    private Vector2 movement;
    private bool jumpIsPressedDown;

    private void Awake()
    {
        inputManager = new InputManager();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = inputManager.Player.Move.ReadValue<Vector2>();
        jumpIsPressedDown = Mathf.Abs(inputManager.Player.Jump.ReadValue<float>()) > 0;
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
            rigidBody.velocity = new Vector2(movement.x * moveSpeed * Time.fixedDeltaTime, rigidBody.velocity.y);
        }
    }

    private void Jump()
    {
        Debug.Log(jumpIsPressedDown);
        if (jumpIsPressedDown)
        {
            rigidBody.AddForce(new Vector2(rigidBody.velocity.x, jumpSpeed * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }
    }
}
