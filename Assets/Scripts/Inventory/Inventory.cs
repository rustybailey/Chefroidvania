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
    public Dictionary<string, bool> AcquiredAbilities { get; private set; }
    public Dictionary<string, bool> AcquiredHealthUpgrades { get; private set; }
    public Dictionary<string, bool> AcquiredIngredients { get; private set; }
    #endregion

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

    // Start is called before the first frame update
    void Start()
    {
        AcquiredAbilities = new Dictionary<string, bool>();
        AcquiredHealthUpgrades = new Dictionary<string, bool>();
        AcquiredIngredients = new Dictionary<string, bool>();
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

    public void AcquireAllAbilities()
    {
        List<string> keys = new List<string>(AcquiredAbilities.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            AcquiredAbilities[keys[i]] = true;
        }
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
}
