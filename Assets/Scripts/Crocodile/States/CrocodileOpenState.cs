using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileOpenState : CrocodileState
{
    public CrocodileOpenState(Crocodile crocodile, string animationBooleanName) : base(crocodile, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(crocodile.openIdleState);
        }
    }
}
