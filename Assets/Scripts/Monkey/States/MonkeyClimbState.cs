using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyClimbState : MonkeyState
{
    private float climbDuration = 1.5f;
    private float countDown;
    private int climbingDirection = 1;
    private float climbSpeed = 2f;

    public MonkeyClimbState(Monkey monkey, string animationBooleanName) : base(monkey, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        countDown = climbDuration;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        monkey.transform.Translate(Vector2.up * climbingDirection * climbSpeed * Time.deltaTime);

        if (countDown < 0f)
        {
            climbingDirection = -climbingDirection;
            stateMachine.ChangeState(monkey.idleState);
        }

        // TODO: Add bottom/top checks and transition to idle if you reach the end of a platform
        // This way we could put a monkey on any height platform
    }
}
