using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanReturnState : FryingPanState
{
    public FryingPanReturnState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        bool facingRight = Vector3.Dot(fryingPan.transform.right, player.GetThrowLocation().transform.position) > 0;
        Debug.Log(System.Convert.ToInt32(facingRight));
        fryingPan.FlipIfNeeded(System.Convert.ToInt32(facingRight));
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // @TODO Flip the frying pan based on the direction it's traveling
        //bool facingRight = Vector3.Dot(fryingPan.transform.right, player.GetThrowLocation().transform.position) > 0;
        //Debug.Log(facingRight);
        //fryingPan.FlipIfNeeded(System.Convert.ToInt32(facingRight));

        float step = fryingPan.GetThrowSpeed() * Time.deltaTime;
        fryingPan.transform.position = Vector3.MoveTowards(fryingPan.transform.position, player.GetThrowLocation().transform.position, step);

        if (Vector3.Distance(fryingPan.transform.position, player.GetThrowLocation().transform.position) < 0.001f)
        {
            // @TODO Hide the frying pan
            fryingPan.StateMachine.ChangeState(fryingPan.idleState);
            player.ReturnFryingPan();
        }
    }
}
