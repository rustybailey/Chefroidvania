using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : FrogState
{
    private float jumpCountdown;

    public FrogIdleState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        jumpCountdown = frog.GetJumpDelay();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        jumpCountdown -= Time.deltaTime;

        if (jumpCountdown <= 0.0f)
        {

            stateMachine.ChangeState(frog.LaunchState);
        }
    }
}
