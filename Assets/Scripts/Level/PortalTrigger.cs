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
            StartCoroutine(AnimatePortalScene(player));
            hasOpened = true;
        }
    }

    private IEnumerator AnimatePortalScene(Player player)
    {
        refrigeratorAnimator.SetBool("shouldOpen", true);

        AudioManager.instance.PlaySoundEffect("EnterPortal");

        yield return new WaitForSeconds(.5f);

        player.StateMachine.ChangeState(player.portalState);

        yield return new WaitForSeconds(1.5f);

        refrigeratorAnimator.SetBool("shouldOpen", false);

        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<LevelLoader>().LoadNextLevelWithTransition();
    }
}
