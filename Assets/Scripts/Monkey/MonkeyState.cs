using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyState : State
{
    protected Monkey monkey;
    protected bool canSeePlayer;

    public MonkeyState(Monkey monkey, string animationBooleanName) : base(monkey.StateMachine, monkey.Animator, animationBooleanName)
    {
        this.monkey = monkey;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        canSeePlayer = Physics2D.OverlapCircle(monkey.transform.position, monkey.playerCheckRadius, monkey.playerLayer);
    }
}
