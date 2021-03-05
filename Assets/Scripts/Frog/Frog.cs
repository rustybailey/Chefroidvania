using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    #region Serialized Variables
    [SerializeField] float jumpDelay = 2.0f;
    [SerializeField] float jumpVerticalVelocity = 3.0f;
    [SerializeField] float jumpHorizontalVelocity = 3.0f;
    [SerializeField] Transform[] PatrolLocations;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    #endregion

    #region Component Variables
    public Animator Animator { get; private set; }
    private Rigidbody2D rigidBody;
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public FrogIdleState IdleState { get; private set; }
    public FrogLaunchState LaunchState { get; private set; }
    public FrogAirState AirState { get; private set; }
    public FrogLandState LandState { get; private set; }
    #endregion

    #region Movement Variables
    private int facingDirection;
    private Vector2 workspace;
    public Vector2 CurrentVelocity { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        IdleState = new FrogIdleState(this, "idle");
        LaunchState = new FrogLaunchState(this, "launch");
        AirState = new FrogAirState(this, "inAir");
        LandState = new FrogLandState(this, "land");
        StateMachine.Initialize(IdleState);
        facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        CurrentVelocity = rigidBody.velocity;

        StateMachine.CurrentState.PhysicsUpdate();
    }

    void OnDrawGizmos()
    {
        // Draw ground check
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.yellow;

        foreach (Transform patrolLocation in PatrolLocations)
        {
            Gizmos.DrawLine(transform.position, patrolLocation.position);
        }
    }

    public void FlipIfNeeded(int facingDirection)
    {
        if (facingDirection != this.facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public float GetJumpDelay()
    {
        return jumpDelay;
    }

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
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

    public float GetJumpVerticalVelocity()
    {
        return jumpVerticalVelocity;
    }

    public float GetJumpHorizontalVelocity()
    {
        return jumpHorizontalVelocity;
    }
}
