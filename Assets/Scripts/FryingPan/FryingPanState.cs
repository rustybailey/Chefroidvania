using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanState : State
{
    protected FryingPan fryingPan;

    public FryingPanState(FryingPan fryingPan, string animationBooleanName) : base(fryingPan.StateMachine, fryingPan.Animator, animationBooleanName)
    {
        this.fryingPan = fryingPan;
    }
}
