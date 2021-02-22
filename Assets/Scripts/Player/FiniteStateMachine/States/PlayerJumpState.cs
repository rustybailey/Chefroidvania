using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animationBooleanName) : base(player, stateMachine, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(player.GetJumpForce() * Time.fixedDeltaTime);
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
