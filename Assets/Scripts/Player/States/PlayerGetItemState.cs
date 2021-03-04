using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetItemState : PlayerState
{
    public PlayerGetItemState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0.0f);
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
