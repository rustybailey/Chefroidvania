using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    private IngredientsUI ingredientsUI;

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
        ingredientsUI = FindObjectOfType<IngredientsUI>();
    }

    public enum ItemType
    {
        Ingredient,
        Ability,
        Health
    }

    public Dictionary<string, bool> AcquiredIngredients = new Dictionary<string, bool>()
    {
        { Ingredients.MILK, true },
        { Ingredients.PINEAPPLE, false },
        { Ingredients.RADISH, false },
        { Ingredients.YAKBURGER, false }
    };

    public Dictionary<string, bool> AcquiredAbilities = new Dictionary<string, bool>()
    {
        { Abilities.FRYING_PAN, false },
        { Abilities.KNIVES, false },
        { Abilities.TENDERIZER, false }
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
                AcquireIngredient(name);
                break;
            case ItemType.Ability:
                AcquireAbility(name);
                break;
            case ItemType.Health:
                HandleHealthUpgrade(name);
                break;
        }
    }

    private void AcquireIngredient(string name)
    {
        AcquiredIngredients[name] = true;

        if (ingredientsUI != null)
        {
            ingredientsUI.ShowIngredient(name + " Ingredient");
        }

        Debug.Log(name + ' ' + AcquiredIngredients[name]);
    }

    private void AcquireAbility(string name)
    {
        string abilityName = name.Replace(" Upgrade", "");
        AcquiredAbilities[abilityName] = true;

        Debug.Log(abilityName + ' ' + AcquiredAbilities[abilityName]);
    }

    public bool HasAbility(string name)
    {
        return AcquiredAbilities.ContainsKey(name) && AcquiredAbilities[name] == true;
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
