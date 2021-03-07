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
    public bool[] abilities;
    public string[] abilityNames;
    public bool[] ingredients;
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
        this.ingredients = new bool[inventory.Ingredients.Count];
        this.ingredientNames = new string[inventory.Ingredients.Count];
        foreach (KeyValuePair<string, bool> ingredient in inventory.Ingredients)
        {
            this.ingredients[i] = ingredient.Value;
            this.ingredientNames[i++] = ingredient.Key;
        }

        i = 0;
        this.abilities = new bool[inventory.Abilities.Count];
        this.abilityNames = new string[inventory.Abilities.Count];
        foreach (KeyValuePair<string, bool> ability in inventory.Abilities)
        {
            this.abilities[i] = ability.Value;
            this.abilityNames[i++] = ability.Key;
        }
    }
}
