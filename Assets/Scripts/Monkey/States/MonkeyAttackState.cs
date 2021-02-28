using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAttackState : MonkeyState
{
    public MonkeyAttackState(Monkey monkey, string animationBooleanName) : base(monkey, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(monkey.idleState);
        }
    }
}
