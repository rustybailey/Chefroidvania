using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanThrowState : FryingPanState
{
    public FryingPanThrowState(FryingPan fryingPan, string animationBooleanName) : base(fryingPan, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //transform.position = position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
