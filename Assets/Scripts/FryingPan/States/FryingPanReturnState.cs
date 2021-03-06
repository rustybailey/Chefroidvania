using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanReturnState : FryingPanState
{
    private bool movingToTheRight;
    private bool playedCatchSound;

    public FryingPanReturnState(FryingPan fryingPan, Player player, string animationBooleanName) : base(fryingPan, player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playedCatchSound = false;

        string[] returnSounds = { "SkilletThrow01", "SkilletThrow02", "SkilletThrow03" };
        AudioManager.instance.PlaySoundEffect(returnSounds[Random.Range(0, returnSounds.Length)]);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        fryingPan.FlipIfNeeded(movingToTheRight ? 1 : -1);

        float step = fryingPan.GetThrowSpeed() * Time.deltaTime;
        fryingPan.transform.position = Vector3.MoveTowards(fryingPan.transform.position, player.GetThrowLocation().transform.position, step);
        float distance = Vector3.Distance(fryingPan.transform.position, player.GetThrowLocation().transform.position);

        if (!playedCatchSound && distance < 3.0f)
        {
            string[] returnSounds = { "CatchSkillet01", "CatchSkillet02", "CatchSkillet03" };
            AudioManager.instance.PlaySoundEffect(returnSounds[Random.Range(0, returnSounds.Length)]);
            playedCatchSound = true;
        }

        if (distance < 1.0f)
        {
            fryingPan.StateMachine.ChangeState(fryingPan.HiddenState);
            player.StateMachine.ChangeState(player.returnFryingPanState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        movingToTheRight = fryingPan.transform.position.x < player.GetThrowLocation().transform.position.x;
    }
}
