using DG.Tweening;
using UnityEngine;

public class PlayerAttackEffect : MonoBehaviour
{
    public float AttackDamage;

    public LayerMask EffectHitLayer;

    public Transform EffectShape;
    public GameObject HitboxObject;
    public float HitboxLifetime;
    public float EffectLifetime;

    public void ColliderHit(Collider hit)
    {
        if (EffectHitLayer.LayerInMask(hit.gameObject.layer))
        {
            GameObject hitTarget = hit.gameObject;
            Debug.LogWarning(hit.gameObject.name + " hit by attack");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DOVirtual.DelayedCall(HitboxLifetime, () => Destroy(HitboxObject));
        DOVirtual.DelayedCall(EffectLifetime, () => Destroy(this.gameObject));

        EffectShape.localScale = Vector3.zero;

        EffectShape.DOScale(Vector3.one, EffectLifetime * 0.65f).SetEase(Ease.OutQuad);
    }
}
