using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemIdleState : TotemState
{
    private float countDown = 2f;

    public TotemIdleState(Totem totem, string animationBooleanName) : base(totem, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER");
        countDown = 2f;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;
        Debug.Log(countDown);

        if (countDown < 0f)
        {
            stateMachine.ChangeState(totem.warningState);
        }
    }
}
