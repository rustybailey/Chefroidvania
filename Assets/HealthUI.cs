using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] GameObject tomatoUiPrefab;

    private List<GameObject> tomatoes = new List<GameObject>();
    private int tomatoSpacing = 100;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Delete children in scene view first
        // TODO: Actually get the player health
        int playerHealth = 3;
        for (int i = 0; i < playerHealth; i++)
        {
            var tomato = InstantiateTomato(tomatoSpacing * i);
            tomatoes.Add(tomato);
        }
    }

    private GameObject InstantiateTomato(int xOffset)
    {
        Vector2 tomatoPos = new Vector2(transform.position.x + xOffset, transform.position.y);
        var tomato = Instantiate(tomatoUiPrefab, tomatoPos, Quaternion.identity);
        tomato.transform.SetParent(gameObject.transform);
        return tomato;
    }

    private void OnEnable()
    {
        PlayerHealth.OnIncreaseMaxHealth += IncreaseMax;
    }

    private void Hurt()
    {
        Debug.Log("DESTROY TOMATO");
    }

    private void FullHeal()
    {
        Debug.Log("DESTROY TOMATO");
    }

    private void IncreaseMax()
    {
        var tomato = InstantiateTomato(tomatoSpacing * tomatoes.Count);
        // TODO: Probably put this in the front if the player is injured
        tomatoes.Add(tomato);
    }
}
