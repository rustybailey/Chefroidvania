using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemIdleState : TotemState
{
    private float idleDuration = 2f;
    private float countDown;
    private bool isInitialIdle = true;

    public TotemIdleState(Totem totem, string animationBooleanName) : base(totem, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        // On start up, have a different idle time so that
        // we don't have all totems shooting at the same time
        // This first idle time will either come from the inspector or be a random time
        if (isInitialIdle)
        {
            countDown = totem.startUpTime > 0 ? totem.startUpTime : Random.Range(1f, 4f);
        }
        else
        {
            countDown = idleDuration;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        if (countDown < 0f)
        {
            isInitialIdle = false;
            stateMachine.ChangeState(totem.warningState);
        }
    }
}
