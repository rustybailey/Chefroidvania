using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetItemIdleState : PlayerState
{
    private float idleDuration = 2f;
    private float countDown;


    public PlayerGetItemIdleState(Player player, string animationBooleanName) : base(player, animationBooleanName)
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
        if (countDown < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
