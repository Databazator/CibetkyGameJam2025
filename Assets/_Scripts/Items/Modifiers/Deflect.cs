using UnityEngine;

/// <summary>
/// Multiplicative deflect chance modifier - lowers the chance of being hit by the given percentage
/// </summary>
public class Deflect : Modifier
{
    public Deflect(float value) : base(ModifierKeyword.Deflect, value) { }

    public override void Apply(GameObject target)
    {
        var deflectComponent = target.GetComponent<PlayerHealth>();
        deflectComponent.AddDeflectionChance(Value);
    }

    public override void Remove(GameObject target)
    {
        var deflectComponent = target.GetComponent<PlayerHealth>();
        deflectComponent.RemoveDeflectionChance(Value);
    }
}