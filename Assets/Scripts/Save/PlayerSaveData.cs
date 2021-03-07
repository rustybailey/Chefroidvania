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
    public bool[] ingredients;
    public bool[] healthUpgrades;
    public int facingDirection;
    #endregion

    public PlayerSaveData(Player player, string saveLocationName)
    {
        Inventory inventory = Inventory.instance;

        sceneName = SceneManager.GetActiveScene().name;
        health = player.Health.GetCurrentHealth();
        abilities = new bool[] { inventory.Abilities["Frying Pan"], inventory.Abilities["Knives"], inventory.Abilities["Tenderizer"] };
        ingredients = new bool[] { inventory.Ingredients["Milk"], inventory.Ingredients["Pineapple"], inventory.Ingredients["Radish"], inventory.Ingredients["Yakburger"] };
        facingDirection = player.FacingDirection;
        healthUpgrades = new bool[inventory.HealthUpgrades.Count];
        this.saveLocationName = saveLocationName;

        int i = 0;
        foreach (KeyValuePair<string, bool> healthUpgrade in inventory.HealthUpgrades)
        {
            this.healthUpgrades[i++] = healthUpgrade.Value;
        }
    }
}
