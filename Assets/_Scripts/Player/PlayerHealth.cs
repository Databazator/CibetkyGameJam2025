using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startingMaxHealth = 100f;
    public float maxHealth = 100f;
    private float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage(float damage)
    {
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
        GameEvents.GameOver();
        gameObject.SetActive(false);
    }
}
