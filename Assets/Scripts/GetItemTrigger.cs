using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemTrigger : MonoBehaviour
{
    [SerializeField] Inventory.ItemType itemType;

    private bool isPlayingGetItemSequence = false;
    private bool isPlayingPortalSequence = false;

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
        string itemName = gameObject.name;

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

        Inventory.instance.AcquireItem(itemType, itemName);

        while (player.StateMachine.CurrentState.Equals(player.getItemState)
            || player.StateMachine.CurrentState.Equals(player.getItemIdleState))
        {
            yield return null;
        }

        if (itemName == Ingredients.MILK)
        {
            yield return StartCoroutine(AnimatePortalScene(player));
        }
        
        Destroy(gameObject);
        isPlayingGetItemSequence = false;
    }

    // Ripped from PortalTrigger so that we can use GetItem and PortalTrigger logic for Milk
    private IEnumerator AnimatePortalScene(Player player)
    {
        // We can't destroy the game object yet since we have more coroutine to do, so let's hide it
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
        player.StateMachine.ChangeState(player.noInputIdleState);
        var refrigeratorAnimator = GameObject.Find("Refrigerator").GetComponent<Animator>();
        refrigeratorAnimator.SetBool("shouldOpen", true);

        yield return new WaitForSeconds(.5f);
        AudioManager.instance.PlaySoundEffect("EnterPortal");
        yield return new WaitForSeconds(1f);

        player.StateMachine.ChangeState(player.portalState);
        yield return new WaitForSeconds(1.5f);

        refrigeratorAnimator.SetBool("shouldOpen", false);
        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<LevelLoader>().LoadNextLevelWithTransition();
    }
}
