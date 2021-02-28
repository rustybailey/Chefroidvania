using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnWallState : PlayerState
{
    private float initialPlayerGravity;

    public PlayerOnWallState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        initialPlayerGravity = player.RigidBody.gravityScale;
        player.RigidBody.gravityScale = 0.0f;
        player.SetVelocityY(0.0f);
    }

    public override void Exit()
    {
        base.Exit();

        player.RigidBody.gravityScale = initialPlayerGravity;
    }
}
