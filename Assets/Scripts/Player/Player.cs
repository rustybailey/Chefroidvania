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
    [SerializeField] GameObject throwLocation;
    #endregion

    #region Component Variables
    private Rigidbody2D rigidBody;
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
    public PlayerPortalState portalState;
    public PlayerHurtState hurtState;
    #endregion

    #region Movement Variables
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    #endregion

    #region Ability Objects
    public FryingPan FryingPan { get; private set; }
    public bool CanThrowFryingPan { get; private set; }
    public bool HasThrownFryingPan { get; private set; }
    public bool IsReturningFryingPan { get; private set; }
    #endregion

    private void Awake()
    {
        InputManager = new InputManager();
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = 1;
        CanThrowFryingPan = true;
        HasThrownFryingPan = false;
        Animator = GetComponent<Animator>();
        idleState = new PlayerIdleState(this, "idle");
        runState = new PlayerRunState(this, "run");
        jumpState = new PlayerJumpState(this, "inAir");
        landState = new PlayerLandState(this, "land");
        inAirState = new PlayerInAirState(this, "inAir");
        throwFryingPanState = new PlayerThrowFryingPanState(this, "throw");
        portalState = new PlayerPortalState(this, "inAir");
        hurtState = new PlayerHurtState(this, "hurt");
        StateMachine.Initialize(idleState);
        FryingPan = FindObjectOfType<FryingPan>();
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

    public void ThrowFryingPan()
    {
        CanThrowFryingPan = false;
        HasThrownFryingPan = true;
    }

    public void BeginReturningFryingPan()
    {
        IsReturningFryingPan = true;
    }

    public void ReturnFryingPan()
    {
        CanThrowFryingPan = true;
        HasThrownFryingPan = false;
        IsReturningFryingPan = false;
    }

    public void StartPortalSucking()
    {
        StateMachine.ChangeState(portalState);
    }

    public void CausePain()
    {

        StateMachine.ChangeState(hurtState);
    }
}
