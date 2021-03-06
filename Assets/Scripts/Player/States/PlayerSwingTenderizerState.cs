using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingTenderizerState : PlayerState
{
    public PlayerSwingTenderizerState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffect("TendWhoosh");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
