using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    protected Player player;
    protected int normalizedMoveX;
    protected int normalizedMoveY;
    protected bool isJumpButtonPressedDown;
    protected bool isGrounded;
    protected bool isYVelocityNearlyZero;
    protected bool isXVelocityNearlyZero;
    protected bool isFryingPanButtonPressedDown;
    protected bool isHeadCollidingWithWall;
    protected bool areFeetCollidingWithWall;
    protected bool isBottomOfPlayerCollidingWithWall;
    protected bool isTopOfPlayerCollidingWithWall;

    public PlayerState(Player player, string animationBooleanName) : base(player.StateMachine, player.Animator, animationBooleanName)
    {
        this.player = player;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 movement = player.InputManager.Player.Move.ReadValue<Vector2>();
        normalizedMoveX = (int)(movement * Vector2.right).normalized.x;
        normalizedMoveY = (int)(movement * Vector2.up).normalized.y;
        isJumpButtonPressedDown = Mathf.Abs(player.InputManager.Player.Jump.ReadValue<float>()) > 0;
        isFryingPanButtonPressedDown = Mathf.Abs(player.InputManager.Player.ThrowFryingPan.ReadValue<float>()) > 0;
        isGrounded = Physics2D.OverlapCircle(player.groundCheck.position, player.groundCheckRadius, player.groundLayer);

        RaycastHit2D wallCheck1Hit = Physics2D.Raycast(
            player.GetWallCheckOrigin1().transform.position,
            player.GetWallCheckOrigin1().transform.right,
            player.GetWallCheckLength(),
            player.GetWallLayer()
        );
        RaycastHit2D wallCheck2Hit = Physics2D.Raycast(
            player.GetWallCheckOrigin2().transform.position,
            player.GetWallCheckOrigin2().transform.right,
            player.GetWallCheckLength(),
            player.GetWallLayer()
        );
        RaycastHit2D lowCheckHit = Physics2D.Raycast(
            player.GetLowCheckOrigin().transform.position,
            player.GetLowCheckOrigin().transform.right,
            player.GetWallCheckLength()
        //player.GetWallLayer()
        );
        RaycastHit2D highCheckHit = Physics2D.Raycast(
            player.GetHighCheckOrigin().transform.position,
            player.GetHighCheckOrigin().transform.right,
            player.GetWallCheckLength()
        //player.GetWallLayer()
        );

        isHeadCollidingWithWall = wallCheck1Hit.collider != null;
        areFeetCollidingWithWall = wallCheck2Hit.collider != null;
        isBottomOfPlayerCollidingWithWall = lowCheckHit.collider != null;
        isTopOfPlayerCollidingWithWall = highCheckHit.collider != null;

        // Apparently tilemaps affect the player's velocity an infinitesimal amount for reasons
        // This thread (https://forum.unity.com/threads/2d-movement-velocity-y-mysteriously-changes.712142/)
        // suggested using Mathf.Approximately to see if the velocity is close enough to zero,
        // but that didn't work, so we're calculating it here
        isYVelocityNearlyZero = Mathf.Abs(player.CurrentVelocity.y) < 0.001f;
        isXVelocityNearlyZero = Mathf.Abs(player.CurrentVelocity.x) < 0.001f;
    }
}
