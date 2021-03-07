using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyWarningState : MonkeyState
{
    public MonkeyWarningState(Monkey monkey, string animationBooleanName) : base(monkey, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffectAtPoint("MonkeyAnger", monkey.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(monkey.attackState);
        }
    }
}
