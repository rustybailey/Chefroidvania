using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerState
{
    public PlayerLandState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (normalizedMoveX != 0)
        {
            stateMachine.ChangeState(player.runState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
