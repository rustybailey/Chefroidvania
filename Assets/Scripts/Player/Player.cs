using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized Variables
    [Header("Movement")]
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] float climbSpeed = 200.0f;
    [SerializeField] float jumpForce = 8.0f;
    [Header("Ground Checks")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    [Header("Frying Pan")]
    [SerializeField] GameObject throwLocation;
    [Header("Wall Checks")]
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
    [Header("Tenderizer")]
    [SerializeField] Transform tenderizerImpactOrigin;
    [SerializeField] float tenderizerImpactRadius;
    #endregion

    #region Component Variables
    public Rigidbody2D RigidBody { get; private set; }
    public InputManager InputManager { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerHealth Health { get; private set; }
    public SpriteRenderer BodySpriteRenderer { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public PlayerNoInputIdleState noInputIdleState;
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
    public PlayerGetItemState getItemState;
    public PlayerGetItemIdleState getItemIdleState;
    #endregion

    #region Movement Variables
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    #endregion

    #region iFrame Variables
    public bool CanBeHurt { get; private set; }
    private float iFrameDuration = 2f;
    #endregion

    #region Ability Objects
    public FryingPan FryingPan { get; private set; }
    [HideInInspector]
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
        FacingDirection = 1;
        isHoldingFryingPan = true;
        CanBeHurt = true;

        RigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<PlayerHealth>();
        BodySpriteRenderer = gameObject.transform.Find("Body").GetComponent<SpriteRenderer>();

        noInputIdleState = new PlayerNoInputIdleState(this, "idle");
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
        getItemState = new PlayerGetItemState(this, "getItem");
        getItemIdleState = new PlayerGetItemIdleState(this, "getItemIdle");
        StateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

#if UNITY_EDITOR
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

        // Draw tenderizer impact radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(tenderizerImpactOrigin.position, tenderizerImpactRadius);
    }
#endif
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

    public void FlipIfNeeded(int normalizedMoveX)
    {
        if (normalizedMoveX != 0 && normalizedMoveX != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    #region Getter Functions
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
    #endregion

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void HandleTenderizerImpact()
    {
        // TODO: Shake screen vertically


        // Play impact sound
        string[] sounds = { "Smack01", "Smack02" };
        AudioManager.instance.PlayRandomSoundEffect(sounds);

        // Cast a circle and trigger destruction of any walls overlapping
        Collider2D[] collisions = Physics2D.OverlapCircleAll(tenderizerImpactOrigin.position, tenderizerImpactRadius);
        foreach (Collider2D collision in collisions)
        {
            var destructibleWall = collision.gameObject.GetComponent<DestructibleWall>();
            if (destructibleWall)
            {
                destructibleWall.TriggerDestruction();
            }

            // TODO: Check for any switches and handle that behavior
        }
    }

    public void HandleIFrames()
    {
        StartCoroutine(InternalHandleIFrames());
    }

    public IEnumerator InternalHandleIFrames()
    {
        CanBeHurt = false;
        float startTime = Time.time;

        while (Time.time - startTime < iFrameDuration)
        {
            BodySpriteRenderer.enabled = !BodySpriteRenderer.enabled;
            yield return new WaitForSeconds(0.025f);
        }

        BodySpriteRenderer.enabled = true;
        CanBeHurt = true;
    }

    // Most other sfx are called within the states
    // These are here so we can called them within the animation
    public void PlayKnifeClimb1()
    {
        AudioManager.instance.PlaySoundEffect("Knife08");
    }

    public void PlayKnifeClimb2()
    {
        AudioManager.instance.PlaySoundEffect("Knife09");
    }

    #region Has Ability Functions
    public bool HasFryingPanAbility()
    {
        return Inventory.instance.HasAbility(Abilities.FRYING_PAN);
    }

    public bool HasKnivesAbility()
    {
        return Inventory.instance.HasAbility(Abilities.KNIVES);
    }

    public bool HasTenderizerAbility()
    {
        return Inventory.instance.HasAbility(Abilities.TENDERIZER);
    }
    #endregion
}
