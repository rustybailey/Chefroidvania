using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemAttackState : TotemState
{
    public TotemAttackState(Totem totem, string animationBooleanName) : base(totem, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffectAtPoint("TotemShoot", totem.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Arrow firing is triggered in the attack animation
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(totem.idleState);
        }
    }
}
