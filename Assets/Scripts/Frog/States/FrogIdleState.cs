using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : FrogState
{
    private float jumpCountdown;
    private int patrolIndex;

    public FrogIdleState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
        patrolIndex = 0;
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

        if (frog.IsHittingWallOrPlatform())
        {
            frog.Flip();
        }

        // @TODO If the frog has reach the current patrol location, go to the next

        if (jumpCountdown <= 0.0f)
        {

            stateMachine.ChangeState(frog.LaunchState);
        }
    }
}
