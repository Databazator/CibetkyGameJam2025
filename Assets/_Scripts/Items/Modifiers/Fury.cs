using UnityEngine;

/// <summary>
/// Multiplicative attack speed modifier
/// </summary>
public class Fury : Modifier
{
    public Fury() : base(ModifierKeyword.Fury) { }

    public override void Apply(GameObject target)
    {
        var attackComponent = target.GetComponent<PlayerAttackAbility>();
        attackComponent.MultiplicateAttackSpeed(1 + Value);
    }

    public override void Remove(GameObject target)
    {
        var attackComponent = target.GetComponent<PlayerAttackAbility>();
        attackComponent.MultiplicateAttackSpeed(1 / (1 + Value));
    }
}