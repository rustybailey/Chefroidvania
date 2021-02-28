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
        // @TODO Check for frying pan ability
        else if (isFryingPanButtonPressedDown && player.isHoldingFryingPan)
        {
            stateMachine.ChangeState(player.throwFryingPanState);
        }
        // @TODO Check for frying pan ability
        else if (isFryingPanButtonPressedDown && player.FryingPan.IsHovering)
        {
            player.FryingPan.StateMachine.ChangeState(player.FryingPan.ExitHoverState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Allow the player to move in the air as long as they are not hitting
        // a wall.
        if (isHittingWall)
        {
            // @TODO Wall climb state

        }
        else
        {
            player.SetVelocityX(normalizedMoveX * player.GetMovementSpeed() * Time.fixedDeltaTime);
        }

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
    }
}
