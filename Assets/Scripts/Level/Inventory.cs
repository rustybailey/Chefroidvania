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
        { "Frying Pan", false },
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
                HandleIngredient(name);
                break;
            case ItemType.Ability:
                HandleAbility(name);
                break;
            case ItemType.Health:
                HandleHealthUpgrade(name);
                break;
        }
    }

    private void HandleIngredient(string name)
    {
        // TODO: Have sillouttes of the ingredients in the top right
        // TODO: When you acquire one, signal to the UI to update with the real image
        Ingredients[name] = true;
        Debug.Log(name + ' ' + Ingredients[name]);
    }

    public delegate void AcquireAbility(string name);
    public event AcquireAbility OnAcquireAbility;

    private void HandleAbility(string name)
    {
        string abilityName = name.Replace(" Upgrade", "");
        Abilities[abilityName] = true;
        OnAcquireAbility?.Invoke(abilityName);

        Debug.Log(abilityName + ' ' + Abilities[abilityName]);
    }

    public delegate void AcquireHealthUpgrade();
    public event AcquireHealthUpgrade OnAcquireHealthUpgrade;
    private void HandleHealthUpgrade(string name)
    {
        // TODO: Update the UI when health is upgraded
        HealthUpgrades[name] = true;
        OnAcquireHealthUpgrade?.Invoke();
        Debug.Log(name + ' ' + HealthUpgrades[name]);
    }
}
