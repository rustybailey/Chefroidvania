using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected StateMachine stateMachine;
    protected bool isAnimationFinished;

    private Animator animator;
    private readonly string animationBooleanName;

    public State(StateMachine stateMachine, Animator animator, string animationBooleanName)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
        this.animationBooleanName = animationBooleanName;
    }

    public virtual void Enter()
    {
        isAnimationFinished = false;
        animator.SetBool(animationBooleanName, true);
    }

    public virtual void Exit()
    {
        animator.SetBool(animationBooleanName, false);
        isAnimationFinished = true;
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void AnimationFinished()
    {
        isAnimationFinished = true;
    }
}
