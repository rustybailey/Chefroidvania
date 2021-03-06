using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanEnterHoverState : FryingPanState
{
    public FryingPanEnterHoverState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        string[] sounds = { "SuccessHit01", "SuccessHit02", "SuccessHit03", "SuccessHit04" };
        AudioManager.instance.PlayRandomSoundEffectAtPoint(sounds, fryingPan.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            fryingPan.StateMachine.ChangeState(fryingPan.HoverState);
        }
    }
}
