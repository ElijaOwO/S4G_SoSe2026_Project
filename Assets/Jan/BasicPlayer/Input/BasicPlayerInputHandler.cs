using UnityEngine;
using UnityEngine.InputSystem;

public class BasicPlayerInputHandler : MonoBehaviour
{
    private BasicPlayerAction playerInputActions;
    private InputAction movementInputAction;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        playerInputActions = new BasicPlayerAction();
        movementInputAction = playerInputActions.BasicPlayerInputMap.Movement;
    }

    private void OnEnable()
    {
        movementInputAction.Enable();

        movementInputAction.performed += OnMovementInput;
        movementInputAction.canceled += OnMovementInput;
    }

    private void OnDisable()
    {
        movementInputAction.Disable();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }
}
