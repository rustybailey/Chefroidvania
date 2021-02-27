using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    #region Serialized Variables
    #endregion

    #region Component Variables
    public Animator Animator { get; private set; }
    #endregion

    #region State Variables
    public StateMachine StateMachine { get; private set; }
    public TotemIdleState idleState;
    public TotemWarningState warningState;
    public TotemAttackState attackState;
    #endregion

    #region Movement Variables
    public int FacingDirection { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new StateMachine();
    }

    // Start is called before the first frame update
    void Start()
    {
        FacingDirection = 1;
        Animator = GetComponent<Animator>();
        idleState = new TotemIdleState(this, "idle");
        warningState = new TotemWarningState(this, "warning");
        attackState = new TotemAttackState(this, "attack");
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

    public void StateAnimationFinished()
    {
        StateMachine.CurrentState.AnimationFinished();
    }

    //public void FireArrow()
    //{
    //    shouldFireArrow = true;
    //}
}
