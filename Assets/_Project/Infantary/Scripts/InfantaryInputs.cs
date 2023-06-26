using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfantaryInputs : MonoBehaviour
{
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 lookInput;
    [SerializeField] private bool fireInput;

    public Vector2 MoveInput { get => moveInput; set => moveInput = value; }
    public Vector2 LookInput { get => lookInput; set => lookInput = value; }
    public bool FireInput { get => fireInput; set => fireInput = value; }

    public void OnGetmoveInputs(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    public void OnGetLookInputs(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    public void OnGetFireInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            fireInput = true;
        else if(ctx.canceled)
            fireInput = false;
    }
}
