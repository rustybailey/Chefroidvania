using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player && !player.StateMachine.CurrentState.Equals(player.getItemState))
        {
            player.StateMachine.ChangeState(player.getItemState);
        }
    }
}
