using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakAttackState : YakState
{
    public YakAttackState(Yak yak, string animationBooleanName) : base(yak, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(yak.resetState);
        }
    }
}
