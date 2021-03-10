using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform startingPoint;
    [SerializeField] Transform endingPoint;
    [SerializeField] Animator heartAnim;
    [SerializeField] GameObject sublimeSundae;

    IEnumerator Start()
    {
        // Make sure all objects (mainly the player) have loaded first
        yield return new WaitForEndOfFrame();

        // Remove control and face customer
        Player player = playerPrefab.GetComponent<Player>();
        player.StateMachine.ChangeState(player.noInputIdleState);
        player.Flip();
        player.transform.position = startingPoint.position;

        // Wait a sec
        yield return new WaitForSeconds(1f);

        // Walk towards counter
        Animator playerAnimator = playerPrefab.GetComponent<Animator>();
        playerAnimator.SetBool("idle", false);
        playerAnimator.SetBool("run", true);

        var distance = Vector2.Distance(playerPrefab.transform.position, endingPoint.position);
        while (distance > 0.001f)
        {
            playerPrefab.transform.position = Vector2.MoveTowards(playerPrefab.transform.position, endingPoint.position, 3f * Time.deltaTime);
            distance = Vector2.Distance(playerPrefab.transform.position, endingPoint.position);
            yield return null;
        }

        // Show the sundae with the get item state
        playerAnimator.SetBool("run", false);
        player.StateMachine.ChangeState(player.getItemState);

        // Move object above the player's hand
        sublimeSundae.transform.position = new Vector2(
            playerPrefab.transform.position.x + (1f * player.FacingDirection),
            playerPrefab.transform.position.y + 1f
        );

        // Wait until you're done showing off
        while (player.StateMachine.CurrentState.Equals(player.getItemState)
            || player.StateMachine.CurrentState.Equals(player.getItemIdleState))
        {
            yield return null;
        }
        Destroy(sublimeSundae);


        // Show heart
        yield return new WaitForSeconds(1f);
        heartAnim.SetBool("appear", true);
        AudioManager.instance.PlaySoundEffectAtPoint("SpeechBubble01", playerPrefab.transform.position);
        yield return new WaitForSeconds(3f);

        // Load credits scene
        FindObjectOfType<LevelLoader>().LoadNextLevelWithTransition();

        yield return null;
    }
}
