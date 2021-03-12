using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        HandleEnter2D(player);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        HandleEnter2D(player);
    }

    private void HandleEnter2D(Player player)
    {
        if (player && player.CanBeHurt)
        {
            player.StateMachine.ChangeState(player.hurtState);
        }
    }
}
