using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakState : State
{
    protected Yak yak;

    public YakState(Yak yak, string animationBooleanName) : base(yak.StateMachine, yak.Animator, animationBooleanName)
    {
        this.yak = yak;
    }
}