using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // »ç¿ë

public class NewInputSystemTest : MonoBehaviour, InputController.IPlayerInputActions
{
    public InputController inputController;

    void Awake()
    {
        inputController = new InputController();
        inputController.PlayerInput.SetCallbacks(this);
    }

    void OnEnable()
    {
        inputController.PlayerInput.Enable();
    }
    void OnDisable()
    {
        inputController.PlayerInput.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log($"OnAttack Started : {context.started}");
        Debug.Log($"OnAttack performed : {context.performed}");
        Debug.Log($"OnAttack canceled : {context.canceled}");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log($"OnMove : {context.ReadValue<Vector2>()}");

    }
}
