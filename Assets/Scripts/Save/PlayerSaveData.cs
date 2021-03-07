using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSaveData
{
    public string sceneName;
    public int health;
    public bool[] abilities;
    public bool[] ingredients;
    public bool[] healthUpgrades;
    public float[] position;
    public int facingDirection;

    public PlayerSaveData(Player player, Vector3 savePosition)
    {
        Inventory inventory = Inventory.instance;

        this.sceneName = SceneManager.GetActiveScene().name;
        this.health = player.Health.GetCurrentHealth();
        this.abilities = new bool[] { inventory.Abilities["Frying Pan"], inventory.Abilities["Knives"], inventory.Abilities["Tenderizer"] };
        this.ingredients = new bool[] { inventory.Ingredients["Milk"], inventory.Ingredients["Pineapple"], inventory.Ingredients["Radish"], inventory.Ingredients["Yakburger"] };
        this.position = new float[] { savePosition.x, savePosition.y, savePosition.z };
        this.facingDirection = player.FacingDirection;
        this.healthUpgrades = new bool[inventory.HealthUpgrades.Count];

        int i = 0;
        foreach (KeyValuePair<string, bool> healthUpgrade in inventory.HealthUpgrades)
        {
            this.healthUpgrades[i++] = healthUpgrade.Value;
        }
    }
}
