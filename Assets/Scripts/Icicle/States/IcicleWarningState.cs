using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleWarningState : IcicleState
{
    public IcicleWarningState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // TODO: Play shake SFX
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(icicle.fallState);
        }
    }
}
