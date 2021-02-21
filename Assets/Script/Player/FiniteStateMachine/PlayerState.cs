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
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void AnimationFinished()
    {
        isAnimationFinished = true;
    }
}
