using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspIdleState : WaspState
{
    private int patrolIndex;

    public WaspIdleState(Wasp wasp, string animationBooleanName) : base(wasp, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        patrolIndex = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float step = wasp.GetMovementSpeed() * Time.deltaTime;

        wasp.transform.position = Vector3.MoveTowards(wasp.transform.position, wasp.PatrolLocations[patrolIndex], step);

        if (Vector3.Distance(wasp.transform.position, wasp.PatrolLocations[patrolIndex]) < 0.001f)
        {
            // Go to the next patrol location
            patrolIndex++;

            if (patrolIndex >= wasp.PatrolLocations.Length)
            {
                patrolIndex = 0;
            }
        }
    }
}
