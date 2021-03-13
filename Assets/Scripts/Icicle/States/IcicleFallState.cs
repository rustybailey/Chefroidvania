using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFallState : IcicleState
{
    private float waitDuration = .5f;
    private float countDown;

    public IcicleFallState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        icicle.Rigidbody.isKinematic = false;
        icicle.Rigidbody.gravityScale = 2.5f;
        icicle.Rigidbody.mass = 100;
        countDown = waitDuration;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();



        // Check for touching ground, if so, change to land state
        countDown -= Time.deltaTime;
        if (countDown <= 0 && icicle.Collider.IsTouchingLayers(icicle.groundLayer))
        {
            stateMachine.ChangeState(icicle.landState);
        }
    }
}
