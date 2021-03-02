using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float climbSpeed = 200.0f;
    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] GameObject throwLocation;
    [SerializeField] GameObject wallCheckOrigin1;
    [SerializeField] GameObject wallCheckOrigin2;
    [SerializeField] GameObject lowCheckOrigin;
    [SerializeField] GameObject highCheckOrigin;
    [SerializeField] float wallCheckLength;
    [SerializeField] LayerMask wallAndPlatformLayers;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] GameObject bigWallCheckOrigin;
    [SerializeField] float bigWallCheckWidth;
    [SerializeField] float bigWallCheckHeight;
    #endregion

    #region Component Variables
    public Rigidbody2D RigidBody { get; private set; }
    public InputManager InputManager { get; private set; }
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerLandState landState;
    public PlayerInAirState inAirState;
    public PlayerThrowFryingPanState throwFryingPanState;
    public PlayerReturnFryingPanState returnFryingPanState;
    public PlayerPortalState portalState;
    public PlayerHurtState hurtState;
    public PlayerWallClimbImpactState wallClimbImpactState;
    public PlayerWallClimbIdleState wallClimbIdleState;
    public PlayerWallClimbState wallClimbState;
    public PlayerSwingTenderizerState swingTenderizerState;
    #endregion

    #region Movement Variables
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    #endregion

    #region Ability Objects
    public FryingPan FryingPan { get; private set; }
    public bool isHoldingFryingPan;
    #endregion

    private void Awake()
    {
        InputManager = new InputManager();
        StateMachine = new StateMachine();
        // The frying pan immediately sets itself to inactive,
        // so we need to grab a reference before it does that
        FryingPan = FindObjectOfType<FryingPan>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = 1;
        isHoldingFryingPan = true;
        Animator = GetComponent<Animator>();
        idleState = new PlayerIdleState(this, "idle");
        runState = new PlayerRunState(this, "run");
        jumpState = new PlayerJumpState(this, "inAir");
        landState = new PlayerLandState(this, "land");
        inAirState = new PlayerInAirState(this, "inAir");
        throwFryingPanState = new PlayerThrowFryingPanState(this, "throw");
        returnFryingPanState = new PlayerReturnFryingPanState(this, "idle");
        portalState = new PlayerPortalState(this, "inAir");
        hurtState = new PlayerHurtState(this, "hurt");
        wallClimbImpactState = new PlayerWallClimbImpactState(this, "wallClimbImpact");
        wallClimbIdleState = new PlayerWallClimbIdleState(this, "wallClimbIdle");
        wallClimbState = new PlayerWallClimbState(this, "wallClimb");
        swingTenderizerState = new PlayerSwingTenderizerState(this, "tenderizer");
        StateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    void OnDrawGizmos()
    {
        // Draw ground check
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawWireCube(bigWallCheckOrigin.transform.position, new Vector3(bigWallCheckWidth, bigWallCheckHeight, 0.0f));

        // Draw wall check ray casts
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection(Vector3.right) * wallCheckLength;
        Gizmos.DrawRay(wallCheckOrigin1.transform.position, direction);
        Gizmos.DrawRay(wallCheckOrigin2.transform.position, direction);
        Gizmos.DrawRay(lowCheckOrigin.transform.position, direction);
        Gizmos.DrawRay(highCheckOrigin.transform.position, direction);
    }

    void FixedUpdate()
    {
        CurrentVelocity = RigidBody.velocity;

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
        RigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public float GetMovementSpeed()
    {
        return moveSpeed;
    }

    public float GetClimbingSpeed()
    {
        return climbSpeed;
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public void FlipIfNeeded(int normalizedMoveX)
    {
        if (normalizedMoveX != 0 && normalizedMoveX != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    public GameObject GetThrowLocation()
    {
        return throwLocation;
    }

    public GameObject GetWallCheckOrigin1()
    {
        return wallCheckOrigin1;
    }

    public GameObject GetWallCheckOrigin2()
    {
        return wallCheckOrigin2;
    }

    public GameObject GetLowCheckOrigin()
    {
        return lowCheckOrigin;
    }

    public GameObject GetHighCheckOrigin()
    {
        return highCheckOrigin;
    }

    public float GetWallCheckLength()
    {
        return wallCheckLength;
    }

    public LayerMask GetWallAndPlatformLayers()
    {
        return wallAndPlatformLayers;
    }

    public LayerMask GetWallLayer()
    {
        return wallLayer;
    }

    public GameObject GetBigWallCheckOrigin()
    {
        return bigWallCheckOrigin;
    }

    public float GetBigWallCheckWidth()
    {
        return bigWallCheckWidth;
    }

    public float GetBigWallCheckHeight()
    {
        return bigWallCheckHeight;
    }
}
