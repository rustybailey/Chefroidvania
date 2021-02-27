using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemState : State
{
    protected Totem totem;

    public TotemState(Totem totem, string animationBooleanName) : base(totem.StateMachine, totem.Animator, animationBooleanName)
    {
        this.totem = totem;
    }
}
