using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleState : State
{
    protected Icicle icicle;

    public IcicleState(Icicle icicle, string animationBooleanName) : base(icicle.StateMachine, icicle.Animator, animationBooleanName)
    {
        this.icicle = icicle;
    }
}