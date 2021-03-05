using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    private bool hasTriggeredDestruction = false;

    public void TriggerDestruction()
    {
        if (hasTriggeredDestruction) { return; }

        StartCoroutine(DestroyAllChildren());
    }

    private IEnumerator DestroyAllChildren()
    {
        hasTriggeredDestruction = true;

        // Loop through all children's animation component and activate the destroy trigger
        Animator[] animators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("destroy");
        }

        yield return new WaitForSeconds(.1f);

        gameObject.GetComponent<CompositeCollider2D>().enabled = false;

        yield return new WaitForSeconds(.7f);

        Destroy(gameObject);
    }
}
