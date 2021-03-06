using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReturnFryingPanState : PlayerState
{

    public PlayerReturnFryingPanState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.isHoldingFryingPan = true;
        stateMachine.ChangeState(player.idleState);
    }
}
