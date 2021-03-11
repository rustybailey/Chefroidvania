using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yak : MonoBehaviour
{
    [SerializeField] public LayerMask playerLayer;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public float sightDistance = 5f;


    public Animator Animator { get; private set; }

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
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * 1f);

        //Gizmos.color = Color.yellow;
        //// Up
        //Gizmos.DrawRay(transform.position, Vector3.up * .8f);
        //// Down
        //Gizmos.DrawRay(transform.position, Vector3.down * .6f);
        //// Top Horz
        //Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + .4f), transform.right * .6f);
        //// Bottom Horz
        //Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y - .5f), transform.right * .6f);
    }
#endif

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }
}
