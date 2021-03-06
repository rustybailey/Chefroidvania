using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogLandState : FrogState
{
    public FrogLandState(Frog frog, string animationBooleanName) : base(frog, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            // If the frog is facing the left and the patrol location is to the
            // right or the frog is facing the right and the patrol location is
            // to the left, go to the next patrol location.
            if ((frog.FacingDirection == -1 && frog.GetCurrentPatrolLocation().x > frog.transform.position.x)
                || (frog.FacingDirection == 1 && frog.GetCurrentPatrolLocation().x < frog.transform.position.x))
            {
                frog.PatrolNext();
            }

            frog.FlipIfNeeded(frog.GetCurrentPatrolLocation().x <= frog.transform.position.x ? -1 : 1);

            stateMachine.ChangeState(frog.IdleState);
        }
    }
}
