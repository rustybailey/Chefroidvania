using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    private bool hasAppliedForce = false;
    private float xForce = 500f;
    private float yForce = 500f;

    public PlayerHurtState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hasAppliedForce = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: Reduce health here

        if (hasAppliedForce && isGrounded && Mathf.Abs(player.CurrentVelocity.x) < 3f)       
        {
            // TODO: Implement the landing hurt animation frame
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!hasAppliedForce)
        {
            player.SetVelocityX(-player.FacingDirection * xForce * Time.fixedDeltaTime);
            player.SetVelocityY(yForce * Time.fixedDeltaTime);
            hasAppliedForce = true;
        }
    }
}
