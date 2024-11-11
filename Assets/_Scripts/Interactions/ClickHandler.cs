using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    InputSystem_Actions _playerControls;
    InputAction _pointerClickAction;
    InputAction _pointerMoveAction;

    void Awake()
    {
        _playerControls = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        _pointerClickAction = _playerControls.Player.Click;
        _pointerMoveAction = _playerControls.UI.Point;

        _pointerMoveAction.Enable();
        _pointerClickAction.Enable();
        _pointerClickAction.started += OnClickPerformed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the action
        _pointerClickAction.started -= OnClickPerformed;
        _pointerClickAction.Disable();
        _pointerMoveAction.Disable();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = _playerControls.UI.Point.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            IPointerHandler pointerHandler = hit.collider.GetComponent<IPointerHandler>();
            if (pointerHandler != null)
            {
                pointerHandler.OnPointerClick();
            }
        }
    }
}

public interface IPointerHandler
{
    void OnPointerClick();
}

