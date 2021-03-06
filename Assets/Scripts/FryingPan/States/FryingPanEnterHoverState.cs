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

        string[] returnSounds = { "SuccessHit01", "SuccessHit02", "SuccessHit03", "SuccessHit04" };
        AudioManager.instance.PlaySoundEffect(returnSounds[Random.Range(0, returnSounds.Length)]);
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
