using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanState : State
{
    protected FryingPan fryingPan;
    protected Player player;

    public FryingPanState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan.StateMachine, fryingPan.Animator, animationBooleanName)
    {
        this.fryingPan = fryingPan;
        this.player = player;
    }
}
