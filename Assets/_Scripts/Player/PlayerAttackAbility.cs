using UnityEngine;

public class PlayerAttackAbility : PlayerAbility
{
    public PlayerAttackEffect AttackEffectObject;
    public Animator PlayerAnimator;
    private bool _isAttacking = false;

    private float _attackTimer = 0f;
    public float AttackDamage = 5f;
    public float AttackDuration;

    public override bool AbilityInUse()
    {
        return _isAttacking;
    }

    public override bool CanUseAbility()
    {
        return !_isAttacking && cooldownTimer <= 0f;
    }

    public override void TriggerAbility(Vector3 direction)
    {
        _isAttacking = true;
        _attackTimer = AttackDuration;

        // play attack anim
        PlayerAnimator?.SetTrigger("Attack");

        //spawn attack effect that deals with damage
        Vector3 attackStartPos = transform.position + direction.normalized * 0.5f;
        Quaternion attackRotation = Quaternion.LookRotation(direction, Vector3.up);

        PlayerAttackEffect attackEffect = Instantiate(AttackEffectObject, attackStartPos, attackRotation) as PlayerAttackEffect;
        attackEffect.AttackDamage = AttackDamage;
    }

    void AttackEnd()
    {
        _isAttacking = false;
        cooldownTimer = Cooldown;
    }

    private void Update()
    {
        if(!_isAttacking || cooldownTimer >= 0f) //not attacking - cd countdown
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if(_isAttacking)
        {
            if(_attackTimer > 0f)
            {
                // do nothing really
            }
            else //end attack
            {
                AttackEnd();
            }

            _attackTimer -= Time.deltaTime;
        }
    }

    public void MultiplicateAttackSpeed(float factor)
    {
        cooldownTimer *= factor;
    }
    
    public void MultiplicateAttackDamage(float factor)
    {
        AttackDamage *= factor;
    }
}
