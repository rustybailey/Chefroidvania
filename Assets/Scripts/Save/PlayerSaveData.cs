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
    public int health;
    public bool[] acquiredAbilities;
    public string[] abilityNames;
    public bool[] acquiredIngredients;
    public string[] ingredientNames;
    public bool[] healthUpgrades;
    public int facingDirection;
    #endregion

    public PlayerSaveData(Player player, string saveLocationName)
    {
        Inventory inventory = Inventory.instance;

        sceneName = SceneManager.GetActiveScene().name;
        health = player.Health.GetCurrentHealth();
        facingDirection = player.FacingDirection;
        healthUpgrades = new bool[inventory.HealthUpgrades.Count];
        this.saveLocationName = saveLocationName;

        int i = 0;
        foreach (KeyValuePair<string, bool> healthUpgrade in inventory.HealthUpgrades)
        {
            this.healthUpgrades[i++] = healthUpgrade.Value;
        }

        i = 0;
        this.acquiredIngredients = new bool[inventory.AcquiredIngredients.Count];
        this.ingredientNames = new string[inventory.AcquiredIngredients.Count];
        foreach (KeyValuePair<string, bool> ingredient in inventory.AcquiredIngredients)
        {
            this.acquiredIngredients[i] = ingredient.Value;
            this.ingredientNames[i++] = ingredient.Key;
        }

        i = 0;
        this.acquiredAbilities = new bool[inventory.AcquiredAbilities.Count];
        this.abilityNames = new string[inventory.AcquiredAbilities.Count];
        foreach (KeyValuePair<string, bool> ability in inventory.AcquiredAbilities)
        {
            this.acquiredAbilities[i] = ability.Value;
            this.abilityNames[i++] = ability.Key;
        }
    }
}
