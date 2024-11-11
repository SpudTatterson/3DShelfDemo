using UnityEngine;
using UnityEngine.InputSystem;

public class DragRotator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _rotationSensitivity = 15f;
    [SerializeField] Vector3 _initialRotation;

    InputSystem_Actions _playerControls;
    InputAction _pointerMoveAction;
    InputAction _pointerClickAction;
    bool _isDragging = false;

    Vector2 _previousMousePosition;

    void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }
    void OnDisable()
    {
        _pointerMoveAction.Disable();
        _pointerClickAction.Disable();
    }
    void OnEnable()
    {
        _pointerMoveAction = _playerControls.UI.Point;
        _pointerMoveAction.performed += OnPointerMove;
        _pointerMoveAction.Enable();

        _pointerClickAction = _playerControls.Player.Click;
        _pointerClickAction.started += OnPointerDown;
        _pointerClickAction.canceled += OnPointerUp;
        _pointerClickAction.Enable();
    }

    void OnPointerDown(InputAction.CallbackContext context)
    {
        _isDragging = true;

        _previousMousePosition = _pointerMoveAction.ReadValue<Vector2>();
    }

    void OnPointerUp(InputAction.CallbackContext context)
    {
        _isDragging = false;
    }

    void OnPointerMove(InputAction.CallbackContext context)
    {
        if (!_isDragging) return;

        Vector2 currentMousePosition = context.ReadValue<Vector2>();

        // Calculate the mouse direction
        Vector2 delta = currentMousePosition - _previousMousePosition;

        _previousMousePosition = currentMousePosition;

        float rotationAmount = delta.x * _rotationSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount, Space.World);
    }

    public void ResetRotator()
    {
        transform.localRotation = Quaternion.Euler(_initialRotation);
    }
}
