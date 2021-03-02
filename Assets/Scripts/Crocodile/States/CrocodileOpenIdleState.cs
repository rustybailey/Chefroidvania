using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileOpenIdleState : CrocodileState
{
    private float idleDuration = 2f;
    private float countDown;
    private bool isInitialIdle = true;

    public CrocodileOpenIdleState(Crocodile crocodile, string animationBooleanName) : base(crocodile, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        countDown = idleDuration;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        if (countDown < 0f)
        {
            stateMachine.ChangeState(crocodile.closeState);
        }
    }
}
