using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] Animator refrigeratorAnimator;

    private bool hasOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasOpened) { return; }

        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            StartCoroutine(AnimatePortalScene(collision.gameObject));
            hasOpened = true;
        }
    }

    private IEnumerator AnimatePortalScene(GameObject player)
    {
        refrigeratorAnimator.SetBool("shouldOpen", true);

        yield return new WaitForSeconds(.5f);

        player.GetComponent<Player>().StartPortalSucking();

        yield return new WaitForSeconds(1.5f);

        refrigeratorAnimator.SetBool("shouldOpen", false);

        // TODO: Then load "Main Scene"
    }
}
