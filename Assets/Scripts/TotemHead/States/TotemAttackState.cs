using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAttackState : TotemState
{
    public TotemAttackState(Totem totem, string animationBooleanName) : base(totem, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: Trigger arrow firing at some point in the animation
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(totem.idleState);
        }
    }
}
