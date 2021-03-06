using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemWarningState : TotemState
{
    public TotemWarningState(Totem totem, string animationBooleanName) : base(totem, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffectAtPoint("TotemAngry", totem.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(totem.attackState);
        }
    }
}
