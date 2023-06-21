using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfantryGamepadController : MonoBehaviour
{
    [SerializeField] private Device controlScheme;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Animator animator;

    private CharacterController characterController;
    private Vector3 movementInput;
    private Vector2 rotationInput;
    private bool isRotationLocked = false;
    private PlayerDeviceController playerDeviceController;
    private PlayerHealth playerHealth;
    
    public void OnGetMoveInputs(InputAction.CallbackContext ctx)
    {
        var inputs = ctx.ReadValue<Vector2>();
        movementInput = new Vector3(inputs.x, 0f, inputs.y);
    }
    
    public void OnGetLookInputs(InputAction.CallbackContext ctx)
    {
        rotationInput = ctx.ReadValue<Vector2>();
    }
    
    public void OnLockRotation(InputAction.CallbackContext ctx)
    {
        isRotationLocked = ctx.action.IsPressed();
    }
    
    private void Start()
    {
        playerDeviceController = GetComponent<PlayerDeviceController>();
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(playerHealth.isDead) return;

        if(controlScheme != playerDeviceController.device) return;
        
        characterController.Move(moveSpeed * Time.deltaTime * movementInput);
        
        if (!isRotationLocked && rotationInput.magnitude > 0.1f)
        {
            float rotationAngle = Mathf.Atan2(rotationInput.x, rotationInput.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 movementRelativeToFacing = transform.InverseTransformDirection(movementInput);
        float forwardAmount = movementRelativeToFacing.z;
        float rightAmount = movementRelativeToFacing.x;

        animator.SetFloat($"vertical", forwardAmount);
        animator.SetFloat($"horizontal", rightAmount);
    }
}
