using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionCheck : MonoBehaviour
{
    [SerializeField] GameObject fridge;

    private bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        fridge.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWon) { return; }

        if (Inventory.instance.HasAllIngredients())
        {
            hasWon = true;
            StartCoroutine(TriggerWinCondition());
        }
    }

    private IEnumerator TriggerWinCondition()
    {
        Debug.Log("YOU DID IT!");

        // Give the get item anim a chance to finish
        yield return new WaitForSeconds(3f);

        // Fade in Fridge
        fridge.SetActive(true);
        fridge.GetComponent<Refrigerator>().FlickerIntoExistence();
    }
}
