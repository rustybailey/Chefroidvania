using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animationBooleanName) : base(player, stateMachine, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.FlipIfNeeded(normalizedMoveX);

        if (normalizedMoveX == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (jumpIsPressedDown && isGrounded && player.CurrentVelocity.y == 0)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(normalizedMoveX * player.GetMovementSpeed() * Time.fixedDeltaTime);
    }
}
