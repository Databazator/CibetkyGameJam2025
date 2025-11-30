using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;


public class GameScreen : UIScreen
{
    ProgressBar _healthBar;

    public void Awake()
    {
        GameEvents.HealthChanged += GEOnHealthChanged;
    }

    public override void Initialize()
    {
        base.Initialize();

        _healthBar = _root.Q("HealthBar") as ProgressBar;
    }

    // private override void Update() {
    //     Base.Update();
    //     if (Time.time - lasTime > 1) {
    //         _healthBar.Update();
    //     }
    // }

    private void GEOnHealthChanged(PlayerHealth playerHealth)
    {
        SetHealthBar(playerHealth);

        DOVirtual.DelayedCall(0.1f, () => {
            // We need to double tap due to race condition in progress bar updates
            SetHealthBar(playerHealth);
        });
    }

    private void SetHealthBar(PlayerHealth playerHealth)
    {
        var newWidth = playerHealth.maxHealth / playerHealth.startingMaxHealth * 100f;
        var newValue = Mathf.Min(playerHealth.CurrentHealth / playerHealth.maxHealth * _healthBar.highValue, 100f);
        var newStyle = new StyleLength(new Length(newWidth, LengthUnit.Percent));
        Debug.Log($"Health changed: newWidth={newWidth}, newValue={newValue}, currentHealth={playerHealth.CurrentHealth}, maxHealth={playerHealth.maxHealth}, originalMaxHealth={playerHealth.startingMaxHealth}");
        _healthBar.style.width = newStyle;
        _healthBar.value = newValue;
    }
}