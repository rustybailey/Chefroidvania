using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleLandState : IcicleState
{
    public IcicleLandState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // TODO: Play Crash SFX
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: Is there even a way to exit this state?
        // TODO: Take away damage dealer script after < 1 second
    }
}
