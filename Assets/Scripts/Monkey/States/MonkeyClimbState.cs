using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyClimbState : MonkeyState
{
    private float climbDuration = 1.5f;
    private float countDown;
    private int startingClimbingDirection = 1;
    private int currentClimbingDirection = 1;
    private float climbSpeed = 2f;

    public MonkeyClimbState(Monkey monkey, string animationBooleanName) : base(monkey, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        countDown = climbDuration;
        currentClimbingDirection = startingClimbingDirection;
        AudioManager.instance.PlaySoundEffectAtPoint("MonkeyClimb", monkey.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        monkey.transform.Translate(Vector2.up * currentClimbingDirection * climbSpeed * Time.deltaTime);

        if (countDown < 0f)
        {
            startingClimbingDirection = -startingClimbingDirection;
            stateMachine.ChangeState(monkey.idleState);
        }
        else if (ShouldChangeDirection())
        {
            currentClimbingDirection = -currentClimbingDirection;
        }
    }

    private bool ShouldChangeDirection()
    {
        bool isTouchingCeiling = Physics2D.Raycast(monkey.transform.position, Vector3.up, .8f, monkey.groundLayer);
        bool isTouchingFloor = Physics2D.Raycast(monkey.transform.position, Vector3.down, .6f, monkey.groundLayer);
        bool isTouchingTopWall = Physics2D.Raycast(new Vector2(monkey.transform.position.x, monkey.transform.position.y + .4f), monkey.transform.right, .6f, monkey.groundLayer);
        bool isTouchingBottomWall = Physics2D.Raycast(new Vector2(monkey.transform.position.x, monkey.transform.position.y - .5f), monkey.transform.right, .6f, monkey.groundLayer);
        bool isTouchingWall = isTouchingTopWall && isTouchingBottomWall;

        return isTouchingCeiling || isTouchingFloor || !isTouchingWall;
    }
}
