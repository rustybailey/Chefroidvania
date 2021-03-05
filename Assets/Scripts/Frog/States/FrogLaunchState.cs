using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogLaunchState : FrogState
{
    public FrogLaunchState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            frog.SetVelocityX(frog.GetJumpHorizontalVelocity());
            frog.SetVelocityY(frog.GetJumpVerticalVelocity());

            stateMachine.ChangeState(frog.AirState);
        }
    }
}
