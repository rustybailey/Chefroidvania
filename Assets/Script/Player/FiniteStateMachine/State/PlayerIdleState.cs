using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animationBooleanName) : base(player, stateMachine, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0.0f);
        player.SetVelocityY(0.0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (normalizedMoveX != 0.0f)
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}
