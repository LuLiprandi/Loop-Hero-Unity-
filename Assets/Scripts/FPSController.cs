using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Contrôleur FPS AZERTY avec rotation souris. Attaché au GameObject Player.</summary>
public class FPSController : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Souris")]
    [SerializeField] private float     _mouseSensitivity = 0.15f;
    [SerializeField] private Transform _cameraTransform;

    private CharacterController _characterController;
    private float               _verticalVelocity;
    private float               _cameraPitch;
    private bool                _canMove = true;

    private const float Gravity      = -9.81f;
    private const float MaxPitch     = 80f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    private void Update()
    {
        HandleGravity();
        if (!_canMove) return;

        HandleMovement();
        HandleMouseLook();
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
        Keyboard kb = Keyboard.current;

        // Positions physiques AZERTY : Z=wKey, Q=aKey, S=sKey, D=dKey
        float horizontal = 0f;
        float vertical   = 0f;

        if (kb.aKey.isPressed) horizontal = -1f;
        if (kb.dKey.isPressed) horizontal =  1f;
        if (kb.wKey.isPressed) vertical   =  1f;
        if (kb.sKey.isPressed) vertical   = -1f;

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 motion = direction * (_moveSpeed * Time.deltaTime);
        motion.y = _verticalVelocity * Time.deltaTime;

        _characterController.Move(motion);
    }

    private void HandleMouseLook()
    {
        if (Mouse.current == null) return;

        Vector2 delta = Mouse.current.delta.ReadValue();

        // Yaw sur le joueur
        transform.Rotate(Vector3.up, delta.x * _mouseSensitivity, Space.World);

        // Pitch sur la caméra, clampé
        if (_cameraTransform != null)
        {
            _cameraPitch -= delta.y * _mouseSensitivity;
            _cameraPitch  = Mathf.Clamp(_cameraPitch, -MaxPitch, MaxPitch);
            _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        }
    }

    /// <summary>Bloque ou débloque les inputs joueur et libère le curseur si désactivé.</summary>
    public void SetMovement(bool enabled)
    {
        _canMove = enabled;

        if (!enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible   = true;
        }
    }
}
