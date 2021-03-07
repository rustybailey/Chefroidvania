using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSaveData
{
    #region Save Data Variables
    public string sceneName;
    public string saveLocationName;
    public string[] acquiredAbilities;
    public string[] acquiredIngredients;
    public string[] acquiredHealthUpgrades;
    #endregion

    public PlayerSaveData(Player player, string saveLocationName)
    {
        Inventory inventory = Inventory.instance;

        sceneName = SceneManager.GetActiveScene().name;
        acquiredAbilities = new string[inventory.AcquiredAbilities.Count];
        acquiredIngredients = new string[inventory.AcquiredIngredients.Count];
        acquiredHealthUpgrades = new string[inventory.AcquiredHealthUpgrades.Count];
        this.saveLocationName = saveLocationName;

        int i = 0;
        foreach (KeyValuePair<string, bool> acquiredHealthUpgrade in inventory.AcquiredHealthUpgrades)
        {
            this.acquiredHealthUpgrades[i++] = acquiredHealthUpgrade.Key;
        }

        i = 0;
        foreach (KeyValuePair<string, bool> acquiredIngredient in inventory.AcquiredIngredients)
        {
            this.acquiredIngredients[i++] = acquiredIngredient.Key;
        }

        i = 0;
        foreach (KeyValuePair<string, bool> acquiredAbility in inventory.AcquiredAbilities)
        {
            this.acquiredAbilities[i++] = acquiredAbility.Key;
        }
    }
}
