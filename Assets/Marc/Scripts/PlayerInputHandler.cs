using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerPrototype playerPrototype;
    
    private PlayerInputBindings playerInputBindings;

    private InputAction movementInputAction;
    
    private InputAction gamepadMovementInputAction;

    public Vector2 Direction { get; private set; }


    private void Awake()
    {
        playerInputBindings = new PlayerInputBindings();
        movementInputAction = playerInputBindings.Player.Movement;
        gamepadMovementInputAction =  playerInputBindings.Player.GamepadMovement;
    }

    private void OnEnable()
    {
        movementInputAction.Enable();

        movementInputAction.performed += OnMovementInput;
        movementInputAction.canceled += OnMovementInput;
        
        gamepadMovementInputAction.Enable();

        gamepadMovementInputAction.performed += OnGamepadMovementInput;
        gamepadMovementInputAction.canceled += OnGamepadMovementInput;
    }


    private void OnDisable()
    {
        movementInputAction.Disable();
        gamepadMovementInputAction.Disable();
    }
    
    private void OnGamepadMovementInput(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }
}
