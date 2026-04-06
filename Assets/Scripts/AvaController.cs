using UnityEngine;
using UnityEngine.InputSystem;

public class AvaController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;

    private CharacterController _characterController;
    private Vector3             _moveDirection;
    private float               _verticalVelocity;
    private bool                _canMove = true;

    private const float Gravity = -9.81f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleGravity();
        if (!_canMove) return;
        HandleMovement();
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded && _verticalVelocity < 0f)
            _verticalVelocity = -2f;
        else
            _verticalVelocity += Gravity * Time.deltaTime;
    }

    private void HandleMovement()
    {
        float horizontal = 0f;
        float vertical   = 0f;

        Keyboard kb = Keyboard.current;

        // Sur AZERTY, Unity Input System mappe les touches par position physique :
        // Z (AZERTY) = wKey, Q (AZERTY) = aKey, S = sKey, D = dKey
        if (kb.aKey.isPressed) horizontal =  1f;
        if (kb.dKey.isPressed) horizontal = -1f;
        if (kb.wKey.isPressed) vertical   = -1f;
        if (kb.sKey.isPressed) vertical   =  1f;

        _moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 motion = _moveDirection * (_moveSpeed * Time.deltaTime);
        motion.y = _verticalVelocity * Time.deltaTime;

        _characterController.Move(motion);
    }

    
    public void SetMovement(bool enabled)
    {
        _canMove       = enabled;
        _moveDirection = Vector3.zero;
    }

    public bool IsMoving() => _canMove && _moveDirection != Vector3.zero;
}
