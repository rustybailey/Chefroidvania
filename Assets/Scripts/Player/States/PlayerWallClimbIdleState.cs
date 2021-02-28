using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbIdleState : PlayerOnWallState
{
    public PlayerWallClimbIdleState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isJumpButtonPressedDown)
        {
            stateMachine.ChangeState(player.jumpState);
        }
        // Ensure that if the player tries to move they are not already at the
        // top or the bottom.
        else if ((normalizedMoveY < 0.0f && isBottomOfPlayerCollidingWithWall)
            || (normalizedMoveY > 0.0f && isTopOfPlayerCollidingWithWall))
        {
            stateMachine.ChangeState(player.wallClimbState);
        }
    }
}
