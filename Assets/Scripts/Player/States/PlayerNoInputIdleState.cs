using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoInputIdleState : PlayerState
{
    // Start is called before the first frame update
    public PlayerNoInputIdleState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0.0f);
    }
}
