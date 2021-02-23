using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : MonoBehaviour
{
    private bool hasOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player && !hasOpened)
        {
            gameObject.GetComponent<Animator>().SetBool("shouldOpen", true);
            hasOpened = true;
        }
    }
}
