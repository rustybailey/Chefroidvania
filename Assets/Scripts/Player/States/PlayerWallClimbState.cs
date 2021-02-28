using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerState
{
    public PlayerWallClimbState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // @TODO Check for jump button and change to in air state

        // @TODO Check for the ground and change to idle state

        // @TODO Check for edge of ledge to climb onto and change to ... state
    }
}
