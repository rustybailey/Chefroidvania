using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffect("Run01");
    }

    public override void Exit()
    {
        base.Exit();

        // TODO: It may sound better if we pause/resume this sound effect
        // instead of stopping it completely
        AudioManager.instance.StopSoundEffect("Run01");
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
        else if (player.HasFryingPanAbility() && isFryingPanButtonPressedDown && player.isHoldingFryingPan)
        {
            stateMachine.ChangeState(player.throwFryingPanState);
        }
        else if (player.HasFryingPanAbility() && isFryingPanButtonPressedDown && player.FryingPan.IsHovering)
        {
            player.FryingPan.StateMachine.ChangeState(player.FryingPan.ExitHoverState);
        }
        else if (player.HasTenderizerAbility() && isTenderizerButtonPressedDown && isGrounded && isYVelocityNearlyZero)
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
