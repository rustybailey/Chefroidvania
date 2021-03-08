using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endingPoint;

    IEnumerator Start()
    {
        // Make sure all objects (mainly the player) have loaded first
        yield return new WaitForEndOfFrame();

        // Remove control and face customer
        Player player = playerPrefab.GetComponent<Player>();
        player.StateMachine.ChangeState(player.noInputIdleState);
        player.Flip();
        player.transform.position = startingPoint.position;

        // TODO: Dummy wait during "conversation" - show speech bubbles here
        yield return new WaitForSeconds(2f);

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
