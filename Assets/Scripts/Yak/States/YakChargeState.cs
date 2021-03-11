using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakChargeState : YakState
{
    public YakChargeState(Yak yak, string animationBooleanName) : base(yak, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: Run forward for X seconds
        // TODO: If you hit the edge or a wall (set up raycasts), turn around (add Flip() method to Yak)

        // TODO: If you hit the player, go to attack state
        // TODO: After X seconds, if you don't hit the player, go to Reset State
    }
}
