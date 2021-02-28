using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanThrowState : FryingPanState
{
    private Vector3 target;

    public FryingPanThrowState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fryingPan.FlipIfNeeded(player.FacingDirection);
        fryingPan.transform.position = player.GetThrowLocation().transform.position;
        target = new Vector3(fryingPan.transform.position.x + (fryingPan.GetThrowDistance() * fryingPan.FacingDirection), fryingPan.transform.position.y, fryingPan.transform.position.z);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float step = fryingPan.GetThrowSpeed() * Time.deltaTime;
        fryingPan.transform.position = Vector3.MoveTowards(fryingPan.transform.position, target, step);

        if (Vector3.Distance(fryingPan.transform.position, target) < 0.001f || fryingPan.HasCollided)
        {
            fryingPan.StateMachine.ChangeState(fryingPan.EnterHoverState);
        }
    }
}
