using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, string animationBooleanName) : base(player, animationBooleanName)
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
        else if (isJumpButtonPressedDown && isGrounded && isYVelocityNearlyZero)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded && player.CurrentVelocity.y < 0)
        {
            stateMachine.ChangeState(player.inAirState);
        }
        // @TODO Check for frying pan ability
        // @TODO Check for frying pan already deployed
        else if (isFryingPanButtonPressedDown)
        {
            stateMachine.ChangeState(player.throwFryingPanState);
        }
    }
}
