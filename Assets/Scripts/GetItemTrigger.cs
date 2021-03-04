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
            // TODO: Do all of this in a coroutine
            // TODO: Wait until the player is grounded before triggering the state change?
            // TODO: Create a getItemIdleState so that this animation can last longer?
            // The idle anim could be the sparkling part of the current anim looping (the sfx lasts 4-5s)

            player.StateMachine.ChangeState(player.getItemState);
            
            // Move object above the player's hand
            gameObject.transform.position = new Vector2(
                collision.transform.position.x + (1f * player.FacingDirection),
                collision.transform.position.y + 1f
            );

            // TODO: Transition to a zoomed in the camera
            // TODO: Add object to player's inventory and/or game state
            // TODO: How do we know what the object is? Pass the gameObject name to the game state method?

            // TODO: Destroy game object
        }
    }
}
