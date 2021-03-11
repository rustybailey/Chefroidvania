using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionCheck : MonoBehaviour
{
    [SerializeField] GameObject fridge;

    private bool hasWon = false;

    // Start is called before the first frame update
    void Awake()
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
        // Give the get item anim a chance to finish
        yield return new WaitForSeconds(3f);

        // Fade in Fridge
        fridge.SetActive(true);
        yield return new WaitForEndOfFrame();
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        fridge.transform.position = new Vector3(playerPos.x + 2.5f, Mathf.Round(playerPos.y - .7f), playerPos.z);
        fridge.GetComponent<Refrigerator>().FlickerIntoExistence();
    }
}
