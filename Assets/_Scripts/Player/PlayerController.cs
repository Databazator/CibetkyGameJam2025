using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions _actions;
    private InputAction _movementAction;
    private InputAction _lookAction;
    private InputAction _attackAction;
    private InputAction _dashAction;
    public InputAction interactAction;

    public float MovementSpeed;
    public float GRAVITY = Physics.gravity.y;
    private Vector2 _lastMovementInput;

    public PlayerAbility DashAbility;
    public PlayerAbility AttackAbility;
    
    private CharacterController _charController;
    public Vector3 LookDirection = Vector3.forward;

    public Transform TrackingDebug;

    public float ControllerLookDeadzone = 0.35f;
    private bool _isMKBUsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove())
        {
            HandleMovement();
        }

        HandleLookDirection();

        if (CanUseAbilities())
        {
            HandleAttack();
            HandleDash();
        }
    }

    void HandleMovement()
    {
        Vector2 input = _movementAction.ReadValue<Vector2>();

        _lastMovementInput = input;

        Vector3 movementInput = new Vector3(input.x, 0, input.y);

        Vector3 movementVector = movementInput * MovementSpeed * Time.deltaTime;
        _charController.Move(movementVector);

        if(!_charController.isGrounded)
        {
            Vector3 gravityForce = Vector3.down * GRAVITY * Time.deltaTime;
            _charController.Move(gravityForce);
        }
    }

    bool CanMove()
    {
        //cant movev if dashing
        if (DashAbility.AbilityInUse())
            return false;

        return true;
    }

    bool CanUseAbilities()
    {
        return !DashAbility.AbilityInUse() && !AttackAbility.AbilityInUse();
    }

    void HandleAttack()
    {
        if(AttackAbility.CanUseAbility())
        {
            if(_attackAction.ReadValue<float>() > 0f)
            {
                AttackAbility.TriggerAbility(LookDirection);
            }
        }
    }

    void HandleDash()
    {
        if (DashAbility.CanUseAbility())
        {
            if (_dashAction.ReadValue<float>() > 0f)
            {
                // dash dir is based on absolute player input, if no input, dash in current look dir
                Vector3 dashDirection = new Vector3(_lastMovementInput.x, 0, _lastMovementInput.y).normalized;
                if(dashDirection == Vector3.zero)
                {
                    dashDirection = LookDirection;
                }
                DashAbility.TriggerAbility(dashDirection);
            }
        }
    }

    void HandleLookDirection()
    {
        if(_isMKBUsed)
        {
            LookDirection = GetMouseLookDirection();
        }
        else
        {
            LookDirection = GetGamepadLookDirection();
        }

        this.transform.rotation = Quaternion.LookRotation(LookDirection, Vector3.up);
    }

    Vector3 GetMouseLookDirection()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Camera mainCam = Camera.main;
        float playerCamDist = Vector3.Distance(mainCam.transform.position, this.transform.position);

        Vector2 mouseScreenPosNormalized = new Vector2(
            Mathf.Clamp01(Utils.RemapRange(mouseScreenPos.x, 0, mainCam.pixelWidth, 0f, 1f)),
            Mathf.Clamp01(Utils.RemapRange(mouseScreenPos.y, 0, mainCam.pixelHeight, 0f, 1f))
            );

        float screenYDistMult = Utils.RemapRange(mouseScreenPosNormalized.y, 0, 1, 0.25f, 1.75f);
        playerCamDist *= screenYDistMult;
        //Debug.Log($"mousePos: {mouseScreenPos}, normalized: {mouseScreenPosNormalized}, multedY: {screenYDistMult}");
        Vector3 mouseWorldSpace = mainCam.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, playerCamDist));

        mouseWorldSpace = new Vector3(mouseWorldSpace.x, transform.position.y, mouseWorldSpace.z);
        TrackingDebug.position = mouseWorldSpace;

        return mouseWorldSpace - transform.position;
    }

    Vector3 GetGamepadLookDirection()
    {
        Vector2 input = _lookAction.ReadValue<Vector2>();

        TrackingDebug.position = transform.position + LookDirection;

        if (input == Vector2.zero || input.magnitude <= ControllerLookDeadzone) // if no input, stay on previous look dir
            return LookDirection;
        return new Vector3(input.x, 0f, input.y);
    }

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();

        _actions = new PlayerInputActions();
        _movementAction = _actions.Player.Move;
        _lookAction = _actions.Player.Look;
        _attackAction = _actions.Player.Attack;
        _dashAction = _actions.Player.Dash;
        interactAction = _actions.Player.Interact;
        InputSystem.onActionChange += InputSystem_onActionChange;
    }

    private void InputSystem_onActionChange(object obj, InputActionChange change)
    {
        if(change == InputActionChange.ActionStarted || change == InputActionChange.ActionPerformed)
        {
            InputAction recievedAction = (InputAction)obj;
            InputDevice device = recievedAction.activeControl.device;

            _isMKBUsed = device.name.Equals("Keyboard") || device.name.Equals("Mouse");
        }
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if the collided object is a projectile
        if (hit.gameObject.CompareTag("EnemyProjectile"))
        {
            var projectile = hit.gameObject.GetComponent<EnemyProjectile>();
            if (projectile != null)
            {
                projectile.HitPlayer(this.gameObject);
            }
        }
    }

    public void MultiplicateMoveSpeed(float percentage)
    {
        MovementSpeed *= percentage;
    }
}