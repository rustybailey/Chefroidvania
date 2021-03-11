using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YakIdleState : YakState
{
    private bool canSeePlayer;
    private GameObject yakChew;

    public YakIdleState(Yak yak, string animationBooleanName) : base(yak, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        yakChew = AudioManager.instance.PlayLoopingSoundEffectAtPoint("YakChew", yak.transform.position);
        AudioManager.instance.PlaySoundEffectAtPoint("YakIdle", yak.transform.position);
    }

    public override void Exit()
    {
        base.Exit();
        Object.Destroy(yakChew);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check if you can see the player. If so, get ready to attack
        canSeePlayer = Physics2D.Raycast(yak.wallCheck.position, yak.transform.right, yak.sightDistance, yak.playerLayer);
        if (canSeePlayer)
        {
            stateMachine.ChangeState(yak.alertState);
        }
    }
}
