using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public EnemySpawning spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void DealDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // Update health bar UI if applicable
        var healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth / maxHealth);
        }
    }

    void Die()
    {
        Debug.Log("Enemy has died.");
        if (spawner != null)
        {
            spawner.DefeatEnemy();
        }
        Destroy(gameObject);
    }
}
