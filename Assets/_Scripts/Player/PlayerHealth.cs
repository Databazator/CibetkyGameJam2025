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
    }

    public void Heal(float amount) 
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
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
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    void Die()
    {
        Debug.Log("Player has died.");
        // Handle player death (e.g., respawn, game over screen, etc.)
        // For now, turn off player controls
        gameObject.SetActive(false);
    }
}
