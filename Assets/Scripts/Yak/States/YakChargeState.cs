using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakChargeState : YakState
{
    private float chargeDuration = 2f;
    private float countDown;
    private float chargeSpeed = 9f;
    private GameObject yakRunSfx;

    public YakChargeState(Yak yak, string animationBooleanName) : base(yak, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        countDown = chargeDuration;
        yakRunSfx = AudioManager.instance.PlayLoopingSoundEffectAtPoint("YakRun", yak.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
        Object.Destroy(yakRunSfx);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        countDown -= Time.deltaTime;

        // Wall (looking right)
        bool isTouchingWall = Physics2D.Raycast(yak.wallCheck.position, yak.transform.right, yak.wallCheckDistance, yak.groundLayer);
        // Floor
        bool isTouchingFloor = Physics2D.Raycast(yak.floorCheck.position, Vector3.down, yak.floorCheckDistance, yak.groundLayer);

        // Turn around before we keep charging if we're touching the wall or not touching the floor
        if (isTouchingWall || !isTouchingFloor)
        {
            yak.Flip();
        }

        // Charge (faster than the player)
        yak.transform.Translate(Vector3.right * chargeSpeed * Time.deltaTime);
        yakRunSfx.transform.position = yak.transform.position;

        // If you hit the player show the attack animation
        if (yak.Hitbox.IsTouchingLayers(yak.playerLayer))
        {
            stateMachine.ChangeState(yak.attackState);
        }
        // Otherwise, keep running until we're done
        else if (countDown < 0)
        {
            stateMachine.ChangeState(yak.resetState);
        }
    }
}
