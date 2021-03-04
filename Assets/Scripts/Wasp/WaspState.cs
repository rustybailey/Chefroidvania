using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspState : State
{
    protected Wasp wasp;
    protected bool canSeePlayer;

    public WaspState(Wasp wasp, string animationBooleanName) : base(wasp.StateMachine, wasp.Animator, animationBooleanName)
    {
        this.wasp = wasp;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        canSeePlayer = Physics2D.OverlapCircle(wasp.transform.position, wasp.GetPlayerCheckRadius(), wasp.GetPlayerLayer());
    }
}
