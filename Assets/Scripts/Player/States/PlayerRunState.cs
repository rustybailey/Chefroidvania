using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, string animationBooleanName) : base(player, animationBooleanName)
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
        else if (isJumpButtonPressedDown && isGrounded && isYVelocityNearlyZero)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded && player.CurrentVelocity.y < 0)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else if (player.hasFryingPanAbility && isFryingPanButtonPressedDown && player.isHoldingFryingPan)
        {
            stateMachine.ChangeState(player.throwFryingPanState);
        }
        else if (player.hasFryingPanAbility && isFryingPanButtonPressedDown && player.FryingPan.IsHovering)
        {
            player.FryingPan.StateMachine.ChangeState(player.FryingPan.ExitHoverState);
        }
        else if (player.hasTenderizerAbility && isTenderizerButtonPressedDown && isGrounded && isYVelocityNearlyZero)
        {
            stateMachine.ChangeState(player.swingTenderizerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocityX(normalizedMoveX * player.GetMovementSpeed() * Time.fixedDeltaTime);
    }
}
