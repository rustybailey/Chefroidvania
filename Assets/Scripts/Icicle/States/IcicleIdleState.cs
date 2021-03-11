using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleIdleState : IcicleState
{
    private bool canSeePlayer;

    public IcicleIdleState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Look for player
        //canSeePlayer = Physics2D.Raycast(icicle.wallCheck.position, icicle.transform.right, icicle.sightDistance, icicle.playerLayer);
        //if (canSeePlayer)
        //{
        //    stateMachine.ChangeState(icicle.warningState);
        //}
    }
}
