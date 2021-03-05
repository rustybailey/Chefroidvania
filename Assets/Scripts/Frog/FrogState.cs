using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogState : State
{
    protected Frog frog;
    protected bool isYVelocityNearlyZero;

    public FrogState(Frog frog, string animationBooleanName) : base(frog.StateMachine, frog.Animator, animationBooleanName)
    {
        this.frog = frog;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Apparently tilemaps affect the player's velocity an infinitesimal amount for reasons
        // This thread (https://forum.unity.com/threads/2d-movement-velocity-y-mysteriously-changes.712142/)
        // suggested using Mathf.Approximately to see if the velocity is close enough to zero,
        // but that didn't work, so we're calculating it here
        isYVelocityNearlyZero = Mathf.Abs(frog.CurrentVelocity.y) < 0.001f;
    }
}
