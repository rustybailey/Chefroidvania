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

        // @TODO Move frying pan to player
        player.MoveFryingPanToHand();
        // @TODO Call frying pan throwToLocation
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
