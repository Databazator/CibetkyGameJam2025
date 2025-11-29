using UnityEngine;

public class RegainAbility : PlayerAbility
{
    public float RegainAmount = 0f;
    
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = transform.parent.GetComponent<PlayerHealth>();
        GameEvents.LevelStarted += OnLevelStarted;
        GameEvents.GameOver += OnGameOver;
    }
    
    private void OnDestroy()
    {
        GameEvents.LevelStarted -= OnLevelStarted;
        GameEvents.GameOver -= OnGameOver;
    }

    public override bool AbilityInUse()
    {
        return true;
    }   

    public override bool CanUseAbility()
    {
        return true;
    }

    public override void TriggerAbility(Vector3 _)
    {        
        if (_playerHealth == null)
            return;

        _playerHealth.IncreaseMaxHealth(RegainAmount);
    }

    public void IncreaseRegainAmount(float amount)
    {
        RegainAmount += amount;
    }

    public void DecreaseRegainAmount(float amount)
    {
        RegainAmount -= amount;
    }

    private void OnLevelStarted()
    {
        TriggerAbility(Vector3.zero);
    }

    private void OnGameOver()
    {
        RegainAmount = 0f;
    }
}
