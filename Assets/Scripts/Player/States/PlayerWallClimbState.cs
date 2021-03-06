using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerOnWallState
{
    private float initialPlayerGravity;

    public PlayerWallClimbState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isJumpButtonPressedDown)
        {
            AudioManager.instance.PlaySoundEffect("Knife05");
            stateMachine.ChangeState(player.jumpState);
        }
        else if (isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (isYVelocityNearlyZero)
        {
            stateMachine.ChangeState(player.wallClimbIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Don't move if the player has reached the top or bottom
        if ((isBottomOfPlayerCollidingWithWall && normalizedMoveY < 0.0f)
            || (isTopOfPlayerCollidingWithWall && normalizedMoveY > 0.0f))
        {
            player.SetVelocityY(normalizedMoveY * player.GetClimbingSpeed() * Time.fixedDeltaTime);
        }
        else
        {
            player.SetVelocityY(0.0f);
        }
    }
}
