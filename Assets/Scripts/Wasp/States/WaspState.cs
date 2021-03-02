using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspState : State
{
    protected Wasp wasp;

    public WaspState(Wasp wasp, string animationBooleanName) : base(wasp.StateMachine, wasp.Animator, animationBooleanName)
    {
        this.wasp = wasp;
    }
}
