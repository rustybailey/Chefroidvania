using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton Variables
    public static Inventory instance;
    #endregion

    #region Inventory Variables
    public enum ItemType
    {
        Ingredient,
        Ability,
        Health
    };
    public Dictionary<string, bool> AcquiredAbilities = new Dictionary<string, bool>();
    public Dictionary<string, bool> AcquiredHealthUpgrades = new Dictionary<string, bool>();
    public Dictionary<string, bool> AcquiredIngredients = new Dictionary<string, bool>();
    #endregion

    private int totalIngredients = 4;

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

    public void AcquireItem(ItemType type, string name)
    {
        switch (type)
        {
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
        AcquiredIngredients[name] = true;
        // This object is not destroyed on load so always look for ingredients
        // UI here instead of Start().
        IngredientsUI ingredientsUI = FindObjectOfType<IngredientsUI>();

        if (ingredientsUI != null)
        {
            ingredientsUI.ShowIngredient(name + " Ingredient");
        }
    }

    private void AcquireAbility(string name)
    {
        string abilityName = name.Replace(" Upgrade", "");
        AcquiredAbilities[abilityName] = true;
    }

    public bool HasAbility(string name)
    {
        return AcquiredAbilities.ContainsKey(name) && AcquiredAbilities[name] == true;
    }

    private void AcquireHealthUpgrade(string name)
    {
        AcquiredHealthUpgrades[name] = true;
        // This object is not destroyed on load so always look for player health
        // here instead of Start().
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.HandleIncreasingMaxHealth();
        }
    }

    public bool HasAllIngredients()
    {
        return AcquiredIngredients.Count == totalIngredients;
    }

    #region DEBUG METHODS
    public void AcquireAllAbilities()
    {
        AcquiredAbilities[Abilities.FRYING_PAN] = true;
        AcquiredAbilities[Abilities.KNIVES] = true;
        AcquiredAbilities[Abilities.TENDERIZER] = true;
    }

    public void AcquireFryingPan()
    {
        AcquiredAbilities[Abilities.FRYING_PAN] = true;
    }

    public void AcquireKnives()
    {
        AcquiredAbilities[Abilities.KNIVES] = true;
    }

    public void AcquireTenderizer()
    {
        AcquiredAbilities[Abilities.TENDERIZER] = true;
    }
    #endregion
}
