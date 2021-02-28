using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanHoverState : FryingPanState
{
    public FryingPanHoverState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fryingPan.StartHovering();
        fryingPan.EnablePlayerPlatform();
    }

    public override void Exit()
    {
        base.Exit();

        fryingPan.StopHovering();
        fryingPan.DisablePlayerPlatform();
    }
}
