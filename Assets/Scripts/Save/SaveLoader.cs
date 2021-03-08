using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader
{
    #region Component Variables
    Player player;
    PlayerHealth playerHealth;
    #endregion

    #region Save Data Variables
    private PlayerSaveData saveData;
    #endregion

    public SaveLoader(Player player = null, PlayerHealth playerHealth = null)
    {
        this.player = player;
        this.playerHealth = playerHealth;
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

        if (playerHealth == null)
        {
            return;
        }

        playerHealth.FullHeal();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        player = Object.FindObjectOfType<Player>();

        if (player == null)
        {
            return;
        }

        string[] acquiredAbilities = saveData.acquiredAbilities;

        for (int i = 0; i < acquiredAbilities.Length; i++)
        {
            acquiredAbilities[i] += " Upgrade";
        }

        AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType.Ability, acquiredAbilities);
        AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType.Health, saveData.acquiredHealthUpgrades);
        AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType.Ingredient, saveData.acquiredIngredients);
        FindSaveLocationAndMovePlayerToIt();
    }

    private void FindSaveLocationAndMovePlayerToIt()
    {
        SaveLocation[] saveLocations = SaveLocation.FindObjectsOfType<SaveLocation>();

        foreach (SaveLocation saveLocation in saveLocations)
        {
            if (saveLocation.GetSaveLocationName() == saveData.saveLocationName)
            {
                saveLocation.MovePlayerTo(player);

                break;
            }
        }
    }

    private void AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType itemType, string[] objectNames)
    {
        for (int i = 0; i < objectNames.Length; i++)
        {
            GameObject gameObject = GameObject.Find(objectNames[i]);

            Inventory.instance.AcquireItem(itemType, objectNames[i]);

            if (gameObject != null)
            {
                Object.Destroy(gameObject);
            }
        }
    }
}
