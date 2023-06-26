using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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
    private PlayerHealth playerHealth;
    [HideInInspector] public InfantaryInputs infantaryInputs;
    
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void SetInfantaryInput(InfantaryInputs inputs)
    {
        infantaryInputs = inputs;
    }

    private void Update()
    {
        GetInputs();
        Move();
    }

    private void GetInputs()
    {
        movementInput = new Vector3(infantaryInputs.MoveInput.x, 0f, infantaryInputs.MoveInput.y);
        rotationInput = infantaryInputs.LookInput;
    }

    private void Move()
    {
        if(playerHealth.isDead) return;
        
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
