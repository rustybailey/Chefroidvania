using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanHiddenState : FryingPanState
{
    public FryingPanHiddenState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fryingPan.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();

        fryingPan.gameObject.SetActive(true);
    }
}
