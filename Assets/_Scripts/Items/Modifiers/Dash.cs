using UnityEngine;

/// <summary>
/// Multiplicative dash distance modifier
/// </summary>
public class Dash : Modifier
{
    public Dash() : base(ModifierKeyword.Dash) { }

    public override void Apply(GameObject target)
    {
        var dashComponent = target.GetComponent<DashAbility>();
        dashComponent.MultiplicateDashDistance(1 + Value);
    }

    public override void Remove(GameObject target)
    {
        var dashComponent = target.GetComponent<DashAbility>();
        dashComponent.MultiplicateDashDistance(1 / (1 + Value));
    }
}