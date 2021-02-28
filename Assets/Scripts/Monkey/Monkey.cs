using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField] public float playerCheckRadius;
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] GameObject bananaPrefab;
    [SerializeField] Transform bananaOrigin;

    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; private set; }
    public MonkeyIdleState idleState;
    public MonkeyClimbState climbState;
    public MonkeyWarningState warningState;
    public MonkeyAttackState attackState;

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        idleState = new MonkeyIdleState(this, "idle");
        climbState = new MonkeyClimbState(this, "climb");
        warningState = new MonkeyWarningState(this, "warning");
        attackState = new MonkeyAttackState(this, "attack");
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
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerCheckRadius);
    }

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    public void ThrowBanana()
    {
        Instantiate(bananaPrefab, bananaOrigin.transform.position, Quaternion.identity);
    }
}
