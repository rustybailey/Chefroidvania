using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemTrigger : MonoBehaviour
{
    [SerializeField] Inventory.ItemType itemType;

    private bool isPlayingGetItemSequence = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !isPlayingGetItemSequence)
        {
            StartCoroutine(GetItemSequence(collision));
        }
    }

    private IEnumerator GetItemSequence(Collider2D collision)
    {
        isPlayingGetItemSequence = true;

        Player player = collision.GetComponent<Player>();

        // Wait until the player is grounded before triggering the state change
        while (!player.IsGrounded())
        {
            yield return null;
        }

        player.StateMachine.ChangeState(player.getItemState);

        // Move object above the player's hand
        gameObject.transform.position = new Vector2(
            collision.transform.position.x + (1f * player.FacingDirection),
            collision.transform.position.y + 1f
        );

        // TODO: Transition to a zoomed in the camera

        Inventory.instance.AcquireItem(itemType, gameObject.name);

        while (player.StateMachine.CurrentState.Equals(player.getItemState)
            || player.StateMachine.CurrentState.Equals(player.getItemIdleState))
        {
            yield return null;
        }

        Destroy(gameObject);
        isPlayingGetItemSequence = false;
    }
}
