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

    private bool jumpIsPressedDown;

    private bool isGrounded = false;

    private Vector2 currentVelocity;
    private Vector2 workspace;
    private int facingDirection;

    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public InputManager InputManager { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        InputManager = new InputManager();
        StateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, StateMachine, "idle");
        runState = new PlayerRunState(this, StateMachine, "run");
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        facingDirection = 1;

        Animator = GetComponent<Animator>();
        StateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        jumpIsPressedDown = Mathf.Abs(InputManager.Player.Jump.ReadValue<float>()) > 0;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Debug.Log("Grounded: " + isGrounded);


        currentVelocity = rigidBody.velocity;

        StateMachine.CurrentState.LogicUpdate();
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void FixedUpdate()
    {
        Jump();

        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnEnable()
    {
        InputManager.Player.Enable();
    }

    private void OnDisable()
    {
        InputManager.Player.Disable();
    }

    private void Jump()
    {
        //Debug.Log(jumpIsPressedDown);
        if (jumpIsPressedDown && isGrounded && rigidBody.velocity.y == 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce * Time.fixedDeltaTime);
        }
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        rigidBody.velocity = workspace;
        currentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);
        rigidBody.velocity = workspace;
        currentVelocity = workspace;
    }

    // @TODO Consider using a player data object
    public float GetMovementSpeed()
    {
        return moveSpeed;
    }

    public void FlipIfNeeded(int normalizedMoveX)
    {
        if (normalizedMoveX != 0 && normalizedMoveX != facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
