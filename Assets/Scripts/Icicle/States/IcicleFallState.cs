using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFallState : IcicleState
{
    public IcicleFallState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check for touching ground, if so, change to land state
    }
}
