using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyIdleState : MonkeyState
{
    private float countDown = 2f;
    private bool isInitialIdle = true;

    public MonkeyIdleState(Monkey monkey, string animationBooleanName) : base(monkey, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // TODO: When we're laying out levels, we may want to be more planned rather than random
        // If so, we should make the initialWait a SerializeField
        countDown = isInitialIdle ? Random.Range(1f, 4f) : 2f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        if (countDown < 0f)
        {
            isInitialIdle = false;
            stateMachine.ChangeState(monkey.climbState);
        }
        else if (canSeePlayer)
        {
            isInitialIdle = false;
            stateMachine.ChangeState(monkey.warningState);
        }
    }
}
