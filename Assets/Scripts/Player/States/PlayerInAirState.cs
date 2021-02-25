using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.FlipIfNeeded(normalizedMoveX);

        if (isGrounded && isYVelocityNearlyZero)
        {
            stateMachine.ChangeState(player.landState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Allow the player to move in the air
        player.SetVelocityX(normalizedMoveX * player.GetMovementSpeed() * Time.fixedDeltaTime);

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
    }
}
