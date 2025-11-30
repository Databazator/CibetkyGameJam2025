using DG.Tweening;
using UnityEngine;

public class DashAbility : PlayerAbility
{
    bool _isDashing = false;
    public float DashDuration;
    public float DashDistance;
    public CharacterController CharController;
    private float _dashTimer;
    private Vector3 _dashDirection;

    

    TrailRenderer _dashTrail;
    private void Awake()
    {
        //CharController = GetComponentInParent<CharacterController>();
        _dashTrail = GetComponent<TrailRenderer>();
        _dashTrail.emitting = false;
    }
    public override bool AbilityInUse()
    {
        return _isDashing;
    }

    public override bool CanUseAbility()
    {
        return cooldownTimer <= 0f && !AbilityInUse();
    }

    public override void TriggerAbility(Vector3 direction)
    {        
        _dashDirection = direction.normalized;
        DashStart();
    }

    void DashStart()
    {
        _isDashing = true;
        _dashTimer = DashDuration;
        //Debug.Log($"Dash start");
        _dashTrail.time = 0.3f;
        _dashTrail.emitting = true;
    }

    void DashEnd()
    {
        _isDashing = false;
        cooldownTimer = Cooldown;
        _dashTrail.DOTime(0f, 0.25f);
        DOVirtual.DelayedCall(0.25f, () => _dashTrail.emitting = false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDashing || cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            //Debug.Log($"Dash cooldown: {cooldownTimer}");
        }
        else if(_isDashing)
        {
            // dashing logic
            if (_dashTimer > 0f) //ongoing dash
            {
                Vector3 dashMovement = DashDistance / DashDuration * _dashDirection * Time.deltaTime;
                CharController.Move(dashMovement);
            }
            else //dash ends
            {
                DashEnd();
            }

            _dashTimer -= Time.deltaTime;
        }
    }

    public void MultiplicateDashDistance(float factor)
    {
        DashDistance *= factor;
    }
}
