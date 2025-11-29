using UnityEngine;
using UnityEngine.UIElements;

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

    private void GEOnHealthChanged(float newHealth)
    {
        _healthBar.value = newHealth * _healthBar.highValue;
    }
}
