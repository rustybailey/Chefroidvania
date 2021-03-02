using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileCloseState : CrocodileState
{
    public CrocodileCloseState(Crocodile crocodile, string animationBooleanName) : base(crocodile, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(crocodile.idleState);
        }
    }
}
