using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    #endregion

    #region Component Variables
    private Rigidbody2D rigidBody;
    public InputManager InputManager { get; private set; }
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    #endregion

    #region Movement Variables
    private Vector2 workspace;
    private int facingDirection;
    public Vector2 CurrentVelocity { get; private set; }
    #endregion

    private void Awake()
    {
        InputManager = new InputManager();
        StateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, StateMachine, "idle");
        runState = new PlayerRunState(this, StateMachine, "run");
        jumpState = new PlayerJumpState(this, StateMachine, "jump");
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
        CurrentVelocity = rigidBody.velocity;

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

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        rigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        rigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    // @TODO Consider using a player data object
    public float GetMovementSpeed()
    {
        return moveSpeed;
    }

    public float GetJumpForce()
    {
        return jumpForce;
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
