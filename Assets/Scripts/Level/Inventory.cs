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
        { "Milk", true },
        { "Pineapple", false },
        { "Radish", false },
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

    public delegate void AcquireIngredient(string name);
    public event AcquireIngredient OnAcquireIngredient;
    private void HandleIngredient(string name)
    {
        Ingredients[name] = true;
        OnAcquireIngredient?.Invoke(name);

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
