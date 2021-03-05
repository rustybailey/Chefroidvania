using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogLandState : FrogState
{
    public FrogLandState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(frog.IdleState);
        }
    }
}
