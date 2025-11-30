using UnityEngine;
using _Scripts.Utils;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public float startingMaxHealth = 100f;
    public float maxHealth = 100f;
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }

    public float DeflectionChance = 0f; // Chance to deflect incoming damage (0 to 1)

    private bool _isDead;
    public bool IsDead => _isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        _isDead = false;

        // DOVirtual.DelayedCall(2f, () => DealDamage(200));
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
        GameEvents.HealthChanged(this);
    }

    public void Heal(float amount) 
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        GameEvents.HealthChanged(this);
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
        GameEvents.HealthChanged(this);
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        GameEvents.HealthChanged(this);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        GameEvents.HealthChanged(this);
    }

    void Die()
    {
        if(_isDead)
        {
            Debug.LogWarning("Player is already you doofus!");
            return;
        }
        _isDead = true;

        Debug.Log("Player has died.");
        // sequence params
        float deathSequenceLength = 3f;
        float desaturateSceneDur = 2f;
        
        //gameObject.SetActive(false);
        Animator anim = GetComponentInChildren<Animator>();
        if(anim != null)
        {
            anim.SetTrigger("Dead");
        }
        // tween saturation to bw
        GameObject[] volumes = GameObject.FindGameObjectsWithTag("GlobalVolume");
        if (volumes.Length >= 1) // should be just one
        {
            Volume volume = volumes[0].GetComponent<Volume>();
            Debug.Log($"Volume found: {volume}");
            ColorAdjustments cadj;
            if(volume.profile.TryGet<ColorAdjustments>(out cadj))
            {
                Debug.Log($"profile found: {volume}");
                float ogSaturation = cadj.saturation.GetValue<float>();
                DOTween.To(() => cadj.saturation.GetValue<float>(), (x) => cadj.saturation.SetValue(new MinFloatParameter(x, -100f, true)), -100f, desaturateSceneDur).SetEase(Ease.InOutQuad);
                // reset saturation to default value after death delay back
                DOVirtual.DelayedCall(deathSequenceLength, () => cadj.saturation.SetValue(new MinFloatParameter(ogSaturation, -100f, true)));
            }
        }
        //Game over happens after player death anim and death effect plays
        DOVirtual.DelayedCall(deathSequenceLength + 0.1f, () => GameEvents.GameOver());
        //GameEvents.GameOver();
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