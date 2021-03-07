using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAttackState : MonkeyState
{
    public MonkeyAttackState(Monkey monkey, string animationBooleanName) : base(monkey, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffectAtPoint("MonkeyThrow", monkey.transform.position);
        string[] sounds = { "MonkeyChirp01", "MonkeyChirp02" };
        AudioManager.instance.PlayRandomSoundEffectAtPoint(sounds, monkey.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Banana throwing is triggered in the attack animation
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(monkey.climbState);
        }
    }
}
