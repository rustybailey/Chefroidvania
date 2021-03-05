using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [SerializeField] bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = startingHealth;
        currentHealth = maxHealth;

        Inventory.instance.OnAcquireHealthUpgrade += HandleIncreasingMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Inventory.instance.OnAcquireHealthUpgrade -= HandleIncreasingMaxHealth;
    }

    public delegate void IncreaseMaxHealth();
    public static event IncreaseMaxHealth OnIncreaseMaxHealth;
    // Increment max health and heal to max
    public void HandleIncreasingMaxHealth()
    {
        maxHealth += 1;
        FullHeal();

        OnIncreaseMaxHealth?.Invoke();
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }

    // Decrease current health from damage dealer
    public void DecreaseHealth()
    {
        if (isInvincible) { return; }

        currentHealth--;

        if (currentHealth <= 0)
        {
            StartCoroutine(TriggerDeath());
        }
    }

    private IEnumerator TriggerDeath()
    {
        Time.timeScale = .5f;

        yield return new WaitForSecondsRealtime(1.5f);

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(.1f);

        FindObjectOfType<LevelLoader>().ReloadScene();
    }
}
