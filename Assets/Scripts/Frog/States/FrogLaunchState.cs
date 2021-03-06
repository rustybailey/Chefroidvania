using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogLaunchState : FrogState
{
    private bool jumpReady;

    public FrogLaunchState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        jumpReady = false;

        //frog.SetVelocityX(frog.GetJumpHorizontalVelocity() * frog.FacingDirection);
        //frog.SetVelocityY(frog.GetJumpVerticalVelocity());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (jumpReady)
        {
            frog.SetVelocityX(frog.GetJumpHorizontalVelocity() * frog.FacingDirection);
            frog.SetVelocityY(frog.GetJumpVerticalVelocity());
        }

        if (isAnimationFinished)
        {
            //frog.SetVelocityX(frog.GetJumpHorizontalVelocity() * frog.FacingDirection);
            //frog.SetVelocityY(frog.GetJumpVerticalVelocity());

            stateMachine.ChangeState(frog.AirState);
        }
    }

    public void JumpReady()
    {
        jumpReady = true;
    }
}
