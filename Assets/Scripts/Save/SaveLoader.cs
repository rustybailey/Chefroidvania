using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader
{
    #region Component Variables
    Player player;
    #endregion

    #region Save Data Variables
    private PlayerSaveData saveData;
    #endregion

    public SaveLoader(Player player)
    {
        this.player = player;
    }

    public void LoadFromPlayerSaveData(PlayerSaveData saveData)
    {
        this.saveData = saveData;

        // If the current active scene is not the same as the saved scene, load
        // the saved scene and use the world position the was saved to move the
        // player to.
        if (SceneManager.GetActiveScene().name != saveData.sceneName)
        {
            if (SceneManager.GetSceneByName(saveData.sceneName) == null)
            {
                return;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(saveData.sceneName);

            return;
        }

        // In this case the scene doesn't change so there is no need to load
        // everything.
        if (player == null)
        {
            return;
        }

        FindSaveLocationAndMovePlayerToIt();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        player = Object.FindObjectOfType<Player>();

        FindAcquiredAbilitiesAndRemoveThem();
        FindHealthUpgradesAndRemoveThem();
        FindIngredientsAndRemoveThem();
        FindSaveLocationAndMovePlayerToIt();

        //foreach (KeyValuePair<string, bool> ingredient in Inventory.instance.Ingredients)
        //{
        //Debug.Log("Post Scene Loaded Ingredient " + ingredient.Key + ": " + ingredient.Value);
        //}

        //foreach (KeyValuePair<string, bool> ability in Inventory.instance.AcquiredAbilities)
        //{
        //Debug.Log("Post Scene Loaded Ability " + ability.Key + ": " + ability.Value);
        //}
    }

    private void FindSaveLocationAndMovePlayerToIt()
    {
        SaveLocation[] saveLocations = SaveLocation.FindObjectsOfType<SaveLocation>();

        foreach (SaveLocation saveLocation in saveLocations)
        {
            if (saveLocation.GetSaveLocationName() == saveData.saveLocationName)
            {
                saveLocation.MovePlayerTo(player);

                // @TODO Change player facing direction

                break;
            }
        }
    }

    private void FindAcquiredAbilitiesAndRemoveThem()
    {
        for (int i = 0; i < saveData.acquiredAbilities.Length; i++)
        {
            if (saveData.acquiredAbilities[i] != true)
            {
                continue;
            }

            // @TODO Is there a better way to handle this appending of "Upgrade"
            // to the name?
            GameObject gameObject = GameObject.Find(saveData.abilityNames[i] + " Upgrade");

            // The ability may not exist in the current scene but the player
            // acquired it at some point so make sure they have it now.
            Inventory.instance.AcquireItem(Inventory.ItemType.Ability, saveData.abilityNames[i]);

            if (gameObject != null)
            {
                Object.Destroy(gameObject);
            }
        }
    }

    private void FindHealthUpgradesAndRemoveThem()
    {

    }

    private void FindIngredientsAndRemoveThem()
    {
        for (int i = 0; i < saveData.acquiredIngredients.Length; i++)
        {
            if (saveData.acquiredIngredients[i] != true)
            {
                continue;
            }

            GameObject gameObject = GameObject.Find(saveData.ingredientNames[i]);

            // The ingredient may not exist in the current scene but the player
            // acquired it at some point so make sure they have it now.
            Inventory.instance.AcquireItem(Inventory.ItemType.Ingredient, saveData.ingredientNames[i]);

            if (gameObject != null)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}
