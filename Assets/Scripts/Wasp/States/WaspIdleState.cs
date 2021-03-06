using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspIdleState : WaspState
{
    private float attackDelay;

    public WaspIdleState(Wasp wasp, string animationBooleanName) : base(wasp, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        attackDelay = wasp.GetAttackDelay();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        attackDelay -= Time.deltaTime;

        if (canSeePlayer)
        {
            if (attackDelay <= 0.0f)
            {
                stateMachine.ChangeState(wasp.AttackState);
            }
        }

        float step = wasp.GetMovementSpeed() * Time.deltaTime;

        // If the wasp can see the player, always face them
        if (canSeePlayer)
        {
            wasp.FlipIfNeeded(wasp.Player.transform.position.x <= wasp.transform.position.x ? 1 : -1);
        }
        // Otherwise face the direction of the patrol location
        else
        {
            wasp.FlipIfNeeded(wasp.GetCurrentPatrolLocation().x <= wasp.transform.position.x ? 1 : -1);
        }

        wasp.transform.position = Vector3.MoveTowards(wasp.transform.position, wasp.GetCurrentPatrolLocation(), step);

        if (Vector3.Distance(wasp.transform.position, wasp.GetCurrentPatrolLocation()) < 0.001f)
        {
            wasp.PatrolNext();
        }
    }
}
