using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private bool hasTriggeredDestruction = false;

    public void TriggerDestruction()
    {
        if (hasTriggeredDestruction) { return; }

        StartCoroutine(Destruction());
    }

    private IEnumerator Destruction()
    {
        hasTriggeredDestruction = true;

        // Trigger this wall piece's animation
        gameObject.GetComponent<Animator>().SetTrigger("destroy");

        // Check for wall piece above this one. If found, trigger destruction.
        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position, Vector2.up, 1f);
        foreach (RaycastHit2D hit in hits)
        {
            var destructibleWall = hit.collider.gameObject.GetComponent<DestructibleWall>();
            if (destructibleWall)
            {
                destructibleWall.TriggerDestruction();
            }
        }

        yield return new WaitForSeconds(.1f);

        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(.7f);

        Destroy(gameObject);
    }
}
