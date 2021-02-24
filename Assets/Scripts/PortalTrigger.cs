using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] Animator refrigeratorAnimator;
    [SerializeField] Transform target;

    private bool hasOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasOpened) { return; }

        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            StartCoroutine(AnimatePortalScene());
            hasOpened = true;
        }
    }

    private IEnumerator AnimatePortalScene()
    {

        refrigeratorAnimator.SetBool("shouldOpen", true);

        yield return new WaitForSeconds(2f);

        // Rotate the player and move towards portal
        // Should this be an animation?
        // Scale to zero or use one of the sparkles to blink him away
        // Close the fridge
        //var player = FindObjectOfType<Player>();
        //player.transform.position = Vector2.MoveTowards(player.transform.position, target.position, 0.5f);

        //return null;
    }
}
