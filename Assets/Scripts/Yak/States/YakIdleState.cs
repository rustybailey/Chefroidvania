using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakIdleState : YakState
{
    public YakIdleState(Yak yak, string animationBooleanName) : base(yak, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // TODO: Cast a ray in front of you. If you see the player, change to ChargeState
    }
}
