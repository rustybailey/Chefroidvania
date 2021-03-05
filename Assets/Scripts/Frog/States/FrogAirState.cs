using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAirState : FrogState
{
    public FrogAirState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (frog.IsGrounded() && isYVelocityNearlyZero)
        {
            stateMachine.ChangeState(frog.IdleState);
        }
    }
}
