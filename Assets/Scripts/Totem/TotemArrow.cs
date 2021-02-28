using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemArrow : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private Animator animator;
    private bool isBeingDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Note: Rotation is set when instantiated by totem
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingDestroyed) { return; }

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeingDestroyed || collision.GetComponent<Totem>()) { return; }

        StartCoroutine(TriggerDestroy());
    }

    private IEnumerator TriggerDestroy()
    {
        isBeingDestroyed = true;
        animator.SetTrigger("destroy");
        yield return new WaitForSeconds(0.1f);

        // Disable the collider after we initiate the destroy animation so that it can't hurt the player
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject);
    }
}
