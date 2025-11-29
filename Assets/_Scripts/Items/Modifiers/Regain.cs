using UnityEngine;

/// <summary>
/// Additive regain modifier
/// </summary>
public class Regain : Modifier
{
    public Regain(float value) : base(ModifierKeyword.Regain, value) { }

    public override void Apply(GameObject target)
    {
        var regainComponent = target.GetComponent<RegainAbility>();
        regainComponent.IncreaseRegainAmount(Value);
    }

    public override void Remove(GameObject target)
    {
        var regainComponent = target.GetComponent<RegainAbility>();
        regainComponent.DecreaseRegainAmount(Value);
    }
}