using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endingPoint;
    [SerializeField] Animator iceCreamAnim;
    [SerializeField] Animator thumbsUpAnim;

    IEnumerator Start()
    {
        // Make sure all objects (mainly the player) have loaded first
        yield return new WaitForEndOfFrame();

        // Remove control and face customer
        Player player = playerPrefab.GetComponent<Player>();
        player.StateMachine.ChangeState(player.noInputIdleState);
        player.Flip();
        player.transform.position = startingPoint.position;

        // Start conversation
        yield return new WaitForSeconds(1f);
        iceCreamAnim.SetBool("appear", true);
        AudioManager.instance.PlaySoundEffectAtPoint("SpeechBubble01", playerPrefab.transform.position);
        yield return new WaitForSeconds(2f);
        iceCreamAnim.SetBool("appear", false);
        yield return new WaitForSeconds(1f);
        thumbsUpAnim.SetBool("appear", true);
        AudioManager.instance.PlaySoundEffectAtPoint("SpeechBubble02", playerPrefab.transform.position);
        yield return new WaitForSeconds(2f);
        thumbsUpAnim.SetBool("appear", false);
        yield return new WaitForSeconds(1f);

        // Turn around and walk away
        player.Flip();
        Animator playerAnimator = playerPrefab.GetComponent<Animator>();
        playerAnimator.SetBool("idle", false);
        playerAnimator.SetBool("run", true);

        var distance = Vector2.Distance(playerPrefab.transform.position, endingPoint.position);
        while (distance > 0.001f)
        {
            playerPrefab.transform.position = Vector2.MoveTowards(playerPrefab.transform.position, endingPoint.position, 5f * Time.deltaTime);
            distance = Vector2.Distance(playerPrefab.transform.position, endingPoint.position);
            yield return null;
        }


        // Give control back to player
        playerAnimator.SetBool("run", false);
        player.StateMachine.ChangeState(player.idleState);

        // TODO: Bonus: Jump up and then go back to idle?

        yield return null;
    }
}
