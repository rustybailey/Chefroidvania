using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    // TODO: Does this need to be a monobehavior?
    private void Awake()
    {
        if (instance != null)
        {
            if (instance == this)
            {
                Destroy(this);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public enum ItemType
    {
        Ingredient,
        Ability,
        Health
    }

    public Dictionary<string, bool> Ingredients = new Dictionary<string, bool>()
    {
        { "Milk", false },
        { "Pineapple", false },
        { "Turnip", false },
        { "Yakburger", false }
    };

    public Dictionary<string, bool> Abilities = new Dictionary<string, bool>()
    {
        { "Skillet", false },
        { "Knives", false },
        { "Tenderizer", false }
    };

    public Dictionary<string, bool> HealthUpgrades = new Dictionary<string, bool>()
    {
        { "Health1", false },
        { "Health2", false },
        { "Health3", false }
    };

    public void AcquireItem(ItemType type, string name)
    {
        switch (type)
        {
            // TODO: Create methods for each type of item to acquire
            case ItemType.Ingredient:
                AcquireIngredient(name);
                break;
            case ItemType.Ability:
                AcquireAbility(name);
                break;
            case ItemType.Health:
                AcquireHealthUpgrade(name);
                break;
        }
    }

    private void AcquireIngredient(string name)
    {
        // TODO: Have sillouttes of the ingredients in the top right
        // TODO: When you acquire one, signal to the UI to update with the real image
        Ingredients[name] = true;
        Debug.Log(name + ' ' + Ingredients[name]);
    }

    private void AcquireAbility(string name)
    {
        // TODO: Call into the player to turn on the ability
        // TODO: Gate all of the states behind whether the player has certain abilities
        Abilities[name] = true;
        Debug.Log(name + ' ' + Abilities[name]);
    }

    private void AcquireHealthUpgrade(string name)
    {
        // TODO: Increase the player's max health
        // TODO: Heal the player's current health to max
        // TODO: Update the UI in the top left
        HealthUpgrades[name] = true;
        Debug.Log(name + ' ' + HealthUpgrades[name]);
    }
}
