using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFallState : IcicleState
{
    private float waitDuration = 1f;
    private float countDown;
    private float initialFallSpeed = 6f;
    private float fallSpeed;
    private float speedIncrease = 0f;

    public IcicleFallState(Icicle icicle, string animationBooleanName) : base(icicle, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // Note: I tried to start the rigidbody as kinematic, switch to dynamic
        // when falling, and then back to kinematic after landing.
        // That worked, except when it hit the player and just sat
        // on his head. So I went the manual route of just moving it downwards.

        //icicle.Rigidbody.isKinematic = false;
        //icicle.Rigidbody.gravityScale = 2;
        //icicle.Rigidbody.mass = 100;
        countDown = waitDuration;
        fallSpeed = initialFallSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();



        // Check for touching ground, if so, change to land state
        countDown -= Time.deltaTime;
        Debug.Log(icicle.Collider.IsTouchingLayers(icicle.groundLayer));
        if (countDown <= 0 && icicle.Collider.IsTouchingLayers(icicle.groundLayer))
        {
            stateMachine.ChangeState(icicle.landState);
        }

        fallSpeed += speedIncrease;
        icicle.transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }
}
