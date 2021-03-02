using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileIdleState : CrocodileState
{
    private float idleDuration = 2.4f;
    private float countDown;
    private bool isInitialIdle = true;

    public CrocodileIdleState(Crocodile crocodile, string animationBooleanName) : base(crocodile, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // TODO: When we're laying out levels, we may want to be more planned rather than random
        // If so, we should make the initialWait a SerializeField
        countDown = isInitialIdle ? Random.Range(1f, 4f) : idleDuration;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        if (countDown < 0f)
        {
            isInitialIdle = false;
            stateMachine.ChangeState(crocodile.openState);
        }
    }
}