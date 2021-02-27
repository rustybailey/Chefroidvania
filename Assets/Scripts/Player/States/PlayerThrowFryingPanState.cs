using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowFryingPanState : PlayerState
{
    public PlayerThrowFryingPanState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.isHoldingFryingPan = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            player.FryingPan.StateMachine.ChangeState(player.FryingPan.ThrowState);
            stateMachine.ChangeState(player.idleState);
        }
    }
}
