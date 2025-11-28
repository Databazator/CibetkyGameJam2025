using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    [SerializeField] protected float Cooldown;
    protected float cooldownTimer;
    public abstract bool AbilityInUse();
    public abstract bool CanUseAbility();
    public abstract void TriggerAbility(Vector3 direction);
}
