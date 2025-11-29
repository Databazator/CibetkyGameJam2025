using UnityEngine;

/// <summary>
/// Multiplicative attack damage modifier
/// </summary>
public class Attack : Modifier
{
    public Attack(float value) : base(ModifierKeyword.Attack, value) { }

    public override void Apply(GameObject target)
    {
        var attackComponent = target.GetComponent<PlayerAttackEffect>();
        attackComponent.MultiplicateAttackDamage(1 + Value);
    }

    public override void Remove(GameObject target)
    {
        var attackComponent = target.GetComponent<PlayerAttackEffect>();
        attackComponent.MultiplicateAttackDamage(1 / (1 + Value));
    }
}