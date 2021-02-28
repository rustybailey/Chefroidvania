using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanExitHoverState : FryingPanState
{
    public FryingPanExitHoverState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            fryingPan.StateMachine.ChangeState(fryingPan.ReturnState);
        }
    }
}
