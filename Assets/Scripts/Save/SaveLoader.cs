using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoader
{
    #region Save Data Variables
    private PlayerSaveData saveData;
    #endregion

    //public SaveLoader(Player player = null, PlayerHealth playerHealth = null)
    public SaveLoader(PlayerSaveData saveData)
    {
        this.saveData = saveData;
    }

    public void LoadFromDeath(Player player, PlayerHealth playerHealth)
    {
        FindSaveLocationAndMovePlayerToIt(player);

        playerHealth.FullHeal();
    }

    public void LoadFromMainMenu()
    {
        if (SceneManager.GetSceneByName(saveData.sceneName) == null)
        {
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(saveData.sceneName);

        return;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        Player player = Object.FindObjectOfType<Player>();

        if (player == null)
        {
            return;
        }

        string[] acquiredAbilities = saveData.acquiredAbilities;

        for (int i = 0; i < acquiredAbilities.Length; i++)
        {
            acquiredAbilities[i] += " Upgrade";
        }

        FindSaveLocationAndMovePlayerToIt(player);
        AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType.Ability, acquiredAbilities);
        AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType.Health, saveData.acquiredHealthUpgrades);
        AcquireObjectsOfTypeAndRemoveThem(Inventory.ItemType.Ingredient, saveData.acquiredIngredients);
    }

    private void FindSaveLocationAndMovePlayerToIt(Player player)
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
