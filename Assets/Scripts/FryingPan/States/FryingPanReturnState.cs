using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanReturnState : FryingPanState
{
    private bool movingToTheRight;

    public FryingPanReturnState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        fryingPan.FlipIfNeeded(movingToTheRight ? 1 : -1);

        float step = fryingPan.GetThrowSpeed() * Time.deltaTime;
        fryingPan.transform.position = Vector3.MoveTowards(fryingPan.transform.position, player.GetThrowLocation().transform.position, step);

        if (Vector3.Distance(fryingPan.transform.position, player.GetThrowLocation().transform.position) < 1.0f)
        {
            fryingPan.StateMachine.ChangeState(fryingPan.hiddenState);
            player.StateMachine.ChangeState(player.returnFryingPanState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        movingToTheRight = fryingPan.transform.position.x < player.GetThrowLocation().transform.position.x;
    }
}
