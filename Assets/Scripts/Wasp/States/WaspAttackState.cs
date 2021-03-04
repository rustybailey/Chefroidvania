using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspAttackState : WaspState
{
    private Vector3 playerPosition;

    public WaspAttackState(Wasp wasp, string animationBooleanName) : base(wasp, animationBooleanName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            playerPosition = wasp.Player.transform.position;

            // Fire 2 shots that go around the player
            Stinger stinger = Object.Instantiate(wasp.GetStingerPrefab(), wasp.GetStingerOrigin().transform.position, Quaternion.identity);
            Stinger stinger2 = Object.Instantiate(wasp.GetStingerPrefab(), wasp.GetStingerOrigin().transform.position, Quaternion.identity);

            // @TODO Adjust this value based on the angle between the wasp and the player
            stinger.FireAt(new Vector3(playerPosition.x + 2, playerPosition.y, playerPosition.z));
            stinger2.FireAt(new Vector3(playerPosition.x - 2, playerPosition.y, playerPosition.z));

            stateMachine.ChangeState(wasp.IdleState);
        }
    }
}
