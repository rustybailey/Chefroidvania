using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalState : PlayerState
{
    private Vector3 target;

    public PlayerPortalState(Player player, string animationBooleanName) : base(player, animationBooleanName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        target = new Vector3(player.transform.position.x, player.transform.position.y - 3f, player.transform.position.z);
    }

    public override void Exit()
    {
        base.Exit();
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.transform.rotation = Quaternion.identity;
        player.transform.localScale = Vector3.one;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.transform.position = Vector3.MoveTowards(player.transform.position, target, 2f * Time.deltaTime);
        if (player.transform.localScale.x > 0)
        {
            player.transform.Rotate(0, 0, 720f * Time.deltaTime);
            player.transform.localScale -= Vector3.one * .5f * Time.deltaTime;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

}
