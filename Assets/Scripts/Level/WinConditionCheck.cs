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
        Vector3 fridgePosition = Vector3.zero;
        bool hasSavedFridgePosition = false;

        // Try to get fridge position from player data
        var saveData = SaveSystem.LoadPlayer();
        if (saveData != null)
        {
            var saveDataFridgeLocation = saveData.refrigeratorLocation;
            // If fridge location x is not a zero, use the location in save file
            if (saveDataFridgeLocation.Length == 3 && saveDataFridgeLocation[0] != 0.0f)
            {
                fridgePosition = new Vector3(saveDataFridgeLocation[0], saveDataFridgeLocation[1], 0);
                hasSavedFridgePosition = true;
            }
        }

        // If it's the first appearance, give the get item anim a chance to finish
        if (!hasSavedFridgePosition)
        {
            yield return new WaitForSeconds(3f);
        }

        // Fade in Fridge
        fridge.SetActive(true);
        yield return new WaitForEndOfFrame();
        if (!hasSavedFridgePosition)
        {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            fridgePosition = new Vector3(Mathf.Round(playerPos.x + 2.5f), Mathf.Round(playerPos.y - .7f), 0);
        }
        fridge.transform.position = fridgePosition;
        fridge.GetComponent<Refrigerator>().FlickerIntoExistence();
    }
}
