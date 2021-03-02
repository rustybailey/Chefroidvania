using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileState : State
{
    protected Crocodile crocodile;

    public CrocodileState(Crocodile crocodile, string animationBooleanName) : base(crocodile.StateMachine, crocodile.Animator, animationBooleanName)
    {
        this.crocodile = crocodile;
    }
}