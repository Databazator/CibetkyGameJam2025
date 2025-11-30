using UnityEngine;

/// <summary>
/// Additive regain modifier
/// </summary>
public class Regain : Modifier
{
    public Regain() : base(ModifierKeyword.Regain) { }

    public override void Apply(GameObject target)
    {
        var regainComponent = target.GetComponentInChildren<RegainAbility>();
        regainComponent.IncreaseRegainAmount(Value);
    }

    public override void Remove(GameObject target)
    {
        var regainComponent = target.GetComponentInChildren<RegainAbility>();
        regainComponent.DecreaseRegainAmount(Value);
    }
}