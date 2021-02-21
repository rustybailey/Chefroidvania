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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (normalizedMoveX != 0)
        {
            stateMachine.ChangeState(player.runState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (jumpIsPressedDown && isGrounded)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
