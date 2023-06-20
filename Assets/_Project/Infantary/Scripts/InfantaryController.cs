using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantaryController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    private Rigidbody rb;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Move the player based on input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed;
        rb.velocity = movement;

        // Rotate the player to face the mouse position
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(cameraRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Vector3 pointToLook = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(pointToLook);
        }

        // Calculate movement animation based on player's facing direction
        Vector3 movementRelativeToFacing = transform.InverseTransformDirection(movement);
        float forwardAmount = movementRelativeToFacing.z;
        float rightAmount = movementRelativeToFacing.x;

        // Update animator parameters
        animator.SetFloat("Forward", forwardAmount);
        animator.SetFloat("Right", rightAmount);
    }
}
