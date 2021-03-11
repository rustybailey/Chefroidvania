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
        AudioManager.instance.PlaySoundEffectAtPoint("YakSwing", yak.transform.position);
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
