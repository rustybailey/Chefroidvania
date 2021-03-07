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
        else if (player.HasFryingPanAbility() && isFryingPanButtonPressedDown && player.isHoldingFryingPan)
        {
            stateMachine.ChangeState(player.throwFryingPanState);
        }
        else if (player.HasFryingPanAbility() && isFryingPanButtonPressedDown && player.FryingPan.IsHovering)
        {
            player.FryingPan.StateMachine.ChangeState(player.FryingPan.ExitHoverState);
        }
        else if (player.HasKnivesAbility() && isHeadCollidingWithWall && areFeetCollidingWithWall && !isGrounded && !isXVelocityNearlyZero)
        {
            stateMachine.ChangeState(player.wallClimbImpactState);
        }
        // If the player is directly next to a wall and jumps they will not move
        // forward.  In that case, once the player begins to fall, begin the
        // wall climb.
        else if (player.HasKnivesAbility() && isHeadCollidingWithWall && areFeetCollidingWithWall && !isGrounded && player.CurrentVelocity.y < 0)
        {
            stateMachine.ChangeState(player.wallClimbImpactState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Allow the player to move in the air as long as they are not hitting
        // a wall.
        if (!isCollidingWithWallsAndPlatforms)
        {
            player.SetVelocityX(normalizedMoveX * player.GetMovementSpeed() * Time.fixedDeltaTime);
        }

        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
    }
}
