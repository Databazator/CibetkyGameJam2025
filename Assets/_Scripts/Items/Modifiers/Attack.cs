using UnityEngine;

/// <summary>
/// Multiplicative attack damage modifier
/// </summary>
public class Attack : Modifier
{
    public Attack() : base(ModifierKeyword.Attack) { }

    public override void Apply(GameObject target)
    {
        var attackComponent = target.GetComponentInChildren<PlayerAttackAbility>();
        attackComponent.MultiplicateAttackDamage(1 + Value);
    }

    public override void Remove(GameObject target)
    {
        var attackComponent = target.GetComponentInChildren<PlayerAttackAbility>();
        attackComponent.MultiplicateAttackDamage(1 / (1 + Value));
    }
}