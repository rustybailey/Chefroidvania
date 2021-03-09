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

    #region Component Variables
    private Player player;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        maxHealth = startingHealth;
        currentHealth = maxHealth;
        player = gameObject.GetComponent<Player>();
    }

    public delegate void IncreaseMaxHealth();
    public static event IncreaseMaxHealth OnIncreaseMaxHealth;
    // Increment max health and heal to max
    public void HandleIncreasingMaxHealth()
    {
        float waitTime = (maxHealth - currentHealth + 1) * .25f;
        maxHealth += 1;

        StartCoroutine(FullHealThenTriggerIncreaseMaxEvent(waitTime));
    }

    public delegate void HealAll();
    public static event HealAll OnFullHeal;
    public void FullHeal()
    {
        currentHealth = maxHealth;

        OnFullHeal?.Invoke();
    }

    public IEnumerator FullHealThenTriggerIncreaseMaxEvent(float waitTime)
    {
        FullHeal();

        yield return new WaitForSeconds(waitTime);

        OnIncreaseMaxHealth?.Invoke();
    }

    public delegate void DecreaseHealthEvent();
    public static event DecreaseHealthEvent OnDecreaseHealth;
    // Decrease current health from damage dealer
    public void DecreaseHealth()
    {
        if (isInvincible) { return; }

        currentHealth--;

        OnDecreaseHealth?.Invoke();

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

        PlayerSaveData playerSaveData = SaveSystem.LoadPlayer();

        if (playerSaveData != null)
        {
            new SaveLoader(playerSaveData).LoadFromDeath(player, this);
        }
        else
        {
            FindObjectOfType<LevelLoader>().ReloadScene();
        }
    }

    public int GetStartingHealth()
    {
        return startingHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
