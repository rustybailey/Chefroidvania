using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspAttackState : WaspState
{
    public WaspAttackState(Wasp wasp, string animationBooleanName) : base(wasp, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffectAtPoint("BeeAngry", wasp.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(wasp.IdleState);
        }
    }
}
