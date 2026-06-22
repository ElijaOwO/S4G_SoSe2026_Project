using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerPrototype playerPrototype;
    
    private PlayerInputBindings playerInputBindings;

    private InputAction movementInputAction;

    public Vector2 Direction { get; private set; }


    private void Awake()
    {
        playerInputBindings = new PlayerInputBindings();
        movementInputAction = playerInputBindings.Player.Movement;
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
