using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public float sightDistance = 10f;
    [SerializeField] public Transform floorCheck;
    [SerializeField] public float floorCheckDistance = 1f;


    public Animator Animator { get; private set; }
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public IcicleIdleState idleState;
    public IcicleWarningState warningState;
    public IcicleFallState fallState;
    public IcicleLandState landState;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        idleState = new IcicleIdleState(this, "idle");
        warningState = new IcicleWarningState(this, "warning");
        fallState = new IcicleFallState(this, "fall");
        landState = new IcicleLandState(this, "land");
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
        //// Line of sight
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * sightDistance);

        //// Wall/edge detectors
        //Gizmos.color = Color.yellow;
        //// Wall (looking right)
        //Gizmos.DrawRay(wallCheck.position, transform.right * wallCheckDistance);
        //// Floor
        //Gizmos.DrawRay(floorCheck.position, Vector3.down * floorCheckDistance);
    }
#endif

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }
}
