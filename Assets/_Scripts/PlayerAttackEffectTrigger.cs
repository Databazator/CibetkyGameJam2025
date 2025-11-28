using UnityEngine;

public class PlayerAttackEffectTrigger : MonoBehaviour
{
    PlayerAttackEffect effect;
    private void Awake()
    {
         effect = GetComponentInParent<PlayerAttackEffect>();
         if(effect == null) Debug.LogError("PlayerAttackEffectTrigger: Effect object in parent not found");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"AttackEffectTrigger: {other.gameObject.name} entered");
        effect?.ColliderHit(other);
    }
}
