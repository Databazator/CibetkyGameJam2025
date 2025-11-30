using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class PlayerAttackEffect : MonoBehaviour
{
    public float AttackDamage;

    public LayerMask EffectHitLayer;
    
    public List<AudioClip> AttackSounds;

    //public Transform EffectShape;
    public GameObject HitboxObject;
    public float HitboxLifetime;
    public float EffectLifetime;

    public void ColliderHit(Collider hit)
    {
        if (EffectHitLayer.LayerInMask(hit.gameObject.layer))
        {
            Debug.LogWarning(hit.gameObject.name + " hit by attack");
            if(hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyHealth>()?.DealDamage(AttackDamage);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //DOVirtual.DelayedCall(HitboxLifetime, () => Destroy(HitboxObject));
        DOVirtual.DelayedCall(EffectLifetime, () => Destroy(this.gameObject));
        if (AttackSounds != null && AttackSounds.Count > 0)
        {
            var tmpGO = new GameObject("TempAudio");
            tmpGO.transform.position = this.transform.position;
            var audioSource = tmpGO.AddComponent<AudioSource>();
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            var clip = AttackSounds[Random.Range(0, AttackSounds.Count)];
            audioSource.PlayOneShot(clip);
            Destroy(tmpGO, clip.length + 5f); // destroy object after clip duration

        }

        //EffectShape.localScale = Vector3.zero;

        //EffectShape.DOScale(Vector3.one, EffectLifetime * 0.65f).SetEase(Ease.OutQuad);
    }
}
