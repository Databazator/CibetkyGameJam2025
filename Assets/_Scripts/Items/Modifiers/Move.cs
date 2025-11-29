using UnityEngine;

/// <summary>
/// Multiplicative movement modifier
/// </summary>
public class Move : Modifier
{
    public Move(float value) : base(ModifierKeyword.Move, value) { }

    public override void Apply(GameObject target)
    {
        var moveComponent = target.GetComponent<PlayerController>();
        // 100+value percentage of speed
        moveComponent.MultiplicateMoveSpeed(Value + 1);
    }

    public override void Remove(GameObject target)
    {
        var moveComponent = target.GetComponent<PlayerController>();

        // If value was previously applied, we are operating at 100+value percentage of speed
        // To revert back to original speed, we need to calculate the adjusted percentage
        moveComponent.MultiplicateMoveSpeed(1 / (1 + Value));
    }
}