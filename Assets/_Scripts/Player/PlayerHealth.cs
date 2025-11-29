using UnityEngine;
using _Scripts.Utils;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public float startingMaxHealth = 100f;
    public float maxHealth = 100f;
    private float currentHealth;

    public float DeflectionChance = 0f; // Chance to deflect incoming damage (0 to 1)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
        if (TryDeflect())
        {
            Debug.Log("Player deflected the damage!");
            return;
        }

        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        GameEvents.HealthChanged(currentHealth / maxHealth);
    }

    public void Heal(float amount) 
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        GameEvents.HealthChanged(currentHealth / maxHealth);
    }

    public void DecreaseMaxHealth(float amount)
    {
        maxHealth -= amount;
        if (maxHealth < 0f)
        {
            Die();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        GameEvents.HealthChanged(currentHealth / maxHealth);
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        GameEvents.HealthChanged(currentHealth / maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        GameEvents.HealthChanged(currentHealth / maxHealth);
    }

    void Die()
    {
        Debug.Log("Player has died.");
        float deathSequence = 2f;
        //Game over happens after player death anim and death effect plays
        DOVirtual.DelayedCall(deathSequence, () => GameEvents.GameOver());
        //gameObject.SetActive(false);
        Animator anim = GetComponentInChildren<Animator>();
        if(anim != null)
        {
            anim.SetBool("Dead", true);
        }
        // tween saturation to bw
        // reset saturation to default value after death delay back
    }

    public bool TryDeflect()
    {
        float roll = RNG.GetRandomFloat();
        return roll < DeflectionChance;
    }

    public void AddDeflectionChance(float amount)
    {
        DeflectionChance += amount;
    }

    public void RemoveDeflectionChance(float amount)
    {
        DeflectionChance -= amount;
        if (DeflectionChance < 0f)
        {
            DeflectionChance = 0f;
        }
    }
}