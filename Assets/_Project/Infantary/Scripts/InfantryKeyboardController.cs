using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfantryKeyboardController : MonoBehaviour
{
    [SerializeField] private Device controlScheme;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask ground;
    //private Rigidbody rb;

    private CharacterController characterController;
    private Camera mainCamera;
    private Vector3 movement;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    public void OnGetMoveInputs(InputAction.CallbackContext ctx)
    {
        var inputs = ctx.ReadValue<Vector2>();
        movement = new Vector3(inputs.x, 0f, inputs.y);
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        characterController.Move(moveSpeed * Time.deltaTime * movement);

        // Rotate the player to face the mouse position
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
       
        if (Physics.Raycast(cameraRay, out RaycastHit hit, Mathf.Infinity, ground))
        {
            Vector3 pointToLook = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(pointToLook);
        }

        // Calculate movement animation based on player's facing direction
        Vector3 movementRelativeToFacing = transform.InverseTransformDirection(movement);
        float forwardAmount = movementRelativeToFacing.z;
        float rightAmount = movementRelativeToFacing.x;

        // Update animator parameters
        animator.SetFloat($"vertical", forwardAmount);
        animator.SetFloat($"horizontal", rightAmount);
    }
}
