using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerInAirState
{
    public PlayerJumpState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySoundEffect("JumpGrunt");
        player.SetVelocityY(player.GetJumpForce() * Time.fixedDeltaTime);
    }
}
