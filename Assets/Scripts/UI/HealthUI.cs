using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] GameObject tomatoUiPrefab;

    private List<GameObject> tomatoes = new List<GameObject>();
    private int tomatoSpacing = 100;
    private bool animatingFullHeal = false;

    // Start is called before the first frame update
    void Start()
    {
        // Clean up any example tomatoes in the scene view
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var player = FindObjectOfType<Player>();
        int playerHealth = player.gameObject.GetComponent<PlayerHealth>().GetMaxHealth();
        for (int i = 0; i < playerHealth; i++)
        {
            var tomato = InstantiateTomato(tomatoSpacing * i);
            tomatoes.Add(tomato);
        }
    }

    private GameObject InstantiateTomato(int xOffset)
    {
        // TODO: Add a yOffset and reset the xOffset if we go over X tomatoes
        Vector2 tomatoPos = new Vector2(transform.position.x + xOffset, transform.position.y);
        var tomato = Instantiate(tomatoUiPrefab, tomatoPos, Quaternion.identity);
        tomato.transform.SetParent(gameObject.transform);
        return tomato;
    }

    private void OnEnable()
    {
        PlayerHealth.OnIncreaseMaxHealth += IncreaseMax;
        PlayerHealth.OnDecreaseHealth += DestroyTomato;
        PlayerHealth.OnFullHeal += RecoverAllTomatoes;
    }

    private void OnDisable()
    {
        PlayerHealth.OnIncreaseMaxHealth -= IncreaseMax;
        PlayerHealth.OnDecreaseHealth -= DestroyTomato;
        PlayerHealth.OnFullHeal -= RecoverAllTomatoes;
    }

    private void DestroyTomato()
    {
        for (int i = tomatoes.Count - 1; i >= 0; i--)
        {
            Animator animator = tomatoes[i].GetComponent<Animator>();
            if (!animator.GetBool("destroy"))
            {
                animator.SetBool("destroy", true);
                return;
            }
        }
    }


    private void RecoverAllTomatoes()
    {
        StartCoroutine(RecoverEachTomato());
    }

    private IEnumerator RecoverEachTomato()
    {
        animatingFullHeal = true;
        for (int i = 0; i < tomatoes.Count; i++)
        {
            Animator animator = tomatoes[i].GetComponent<Animator>();
            if (animator.GetBool("destroy"))
            {
                animator.SetBool("destroy", false);
                yield return new WaitForSeconds(.25f);
            }
        }
        animatingFullHeal = false;
    }

    private void IncreaseMax()
    {
        StartCoroutine(WaitToIncreaseMax());
    }

    private IEnumerator WaitToIncreaseMax()
    {
        if (animatingFullHeal)
        {
            yield return null;
        }

        var tomato = InstantiateTomato(tomatoSpacing * tomatoes.Count);
        tomatoes.Add(tomato);
    }
}
