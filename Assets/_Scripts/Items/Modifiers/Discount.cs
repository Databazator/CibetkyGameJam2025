using UnityEngine;

/// <summary>
/// Additive discount modifier
/// </summary>
public class Discount : Modifier
{
    public Discount() : base(ModifierKeyword.Discount) { }

    public override void Apply(GameObject target)
    {
        var regainComponent = target.GetComponent<DiscountAbility>();
        regainComponent.IncreaseDiscountAmount(Value);
    }

    public override void Remove(GameObject target)
    {
        var regainComponent = target.GetComponent<DiscountAbility>();
        regainComponent.DecreaseDiscountAmount(Value);
    }
}