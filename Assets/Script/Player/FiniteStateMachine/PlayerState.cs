using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected int normalizedMoveX;
    protected int normalizedMoveY;
    protected bool jumpIsPressedDown;
    protected bool isGrounded;
    protected bool isAnimationFinished;
    protected bool isYVelocityNearlyZero;

    private string animationBooleanName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animationBooleanName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationBooleanName = animationBooleanName;
    }

    public virtual void Enter()
    {
        isAnimationFinished = false;
        player.Animator.SetBool(animationBooleanName, true);
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animationBooleanName, false);
        isAnimationFinished = true;
    }

    public virtual void LogicUpdate()
    {
        Vector2 movement = player.InputManager.Player.Move.ReadValue<Vector2>();
        normalizedMoveX = (int)(movement * Vector2.right).normalized.x;
        normalizedMoveY = (int)(movement * Vector2.up).normalized.y;
        jumpIsPressedDown = Mathf.Abs(player.InputManager.Player.Jump.ReadValue<float>()) > 0;

        isGrounded = Physics2D.OverlapCircle(player.groundCheck.position, player.groundCheckRadius, player.groundLayer);

        // Apparently tilemaps affect the player's velocity an infinitesimal amount for reasons
        // This thread (https://forum.unity.com/threads/2d-movement-velocity-y-mysteriously-changes.712142/)
        // suggested using Mathf.Approximately to see if the velocity is close enough to zero,
        // but that didn't work, so we're calculating it here
        isYVelocityNearlyZero = Mathf.Abs(player.CurrentVelocity.y) < 0.001f;
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void AnimationFinished()
    {
        isAnimationFinished = true;
    }
}
