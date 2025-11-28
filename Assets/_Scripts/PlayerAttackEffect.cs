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
        Debug.LogWarning(hit.gameObject.name + " as hit");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DOVirtual.DelayedCall(HitboxLifetime, () => Destroy(HitboxObject));
        DOVirtual.DelayedCall(EffectLifetime, () => Destroy(gameObject));

        EffectShape.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
       EffectShape.DOScale(Vector3.one, EffectLifetime * 0.35f).SetEase(Ease.OutQuad);
    }
}
