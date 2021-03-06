using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbImpactState : PlayerOnWallState
{
    public PlayerWallClimbImpactState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffect("Knife04");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished || normalizedMoveY != 0.0f)
        {
            stateMachine.ChangeState(player.wallClimbIdleState);
        }
    }
}
