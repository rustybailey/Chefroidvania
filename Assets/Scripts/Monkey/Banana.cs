using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    private Animator animator;
    private bool isBeingDestroyed = false;
    private Vector3 travelDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        GameObject player = GameObject.Find("Player");
        travelDirection = (player.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingDestroyed) { return; }

        transform.Translate(travelDirection * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeingDestroyed || collision.GetComponent<Monkey>()) { return; }

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