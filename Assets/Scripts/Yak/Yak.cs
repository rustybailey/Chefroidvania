using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yak : MonoBehaviour
{
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public float sightDistance = 9f;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public Transform floorCheck;
    [SerializeField] public float wallCheckDistance = .5f;
    [SerializeField] public float floorCheckDistance = 1f;


    public Animator Animator { get; private set; }
    public Collider2D Hitbox { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public YakIdleState idleState;
    public YakAlertState alertState;
    public YakChargeState chargeState;
    public YakAttackState attackState;
    public YakResetState resetState;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Hitbox = GetComponent<Collider2D>();
        idleState = new YakIdleState(this, "idle");
        alertState = new YakAlertState(this, "alert");
        chargeState = new YakChargeState(this, "charge");
        attackState = new YakAttackState(this, "attack");
        resetState = new YakResetState(this, "reset");
        StateMachine.Initialize(idleState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        // Line of sight
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheck.position, transform.right * sightDistance);

        // Wall/edge detectors
        Gizmos.color = Color.yellow;
        // Wall (looking right)
        Gizmos.DrawRay(wallCheck.position, transform.right * wallCheckDistance);
        // Floor
        Gizmos.DrawRay(floorCheck.position, Vector3.down * floorCheckDistance);
    }
#endif

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    public void Flip()
    {
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
