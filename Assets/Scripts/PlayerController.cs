using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravityScale = 1f;

    private float horizontalInput;
    private float verticalInput;
    public float rotationSpeed = 5f;

    private Vector3 moveDirection;
    public bool isGrounded;
    public float gravity = -9.81f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Get input for movement
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Get the camera's forward and right directions
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Flatten the camera vectors (remove vertical component)
        cameraForward.y = 0;
        cameraRight.y = 0;


        // Calculate movement direction relative to the camera
        Vector3 inputDirection = (cameraForward * verticalInput + cameraRight * horizontalInput);

        // If there's input, move; otherwise, stop
        if (horizontalInput != 0 || verticalInput != 0)
        {
            moveDirection = inputDirection * speed;
        }
        else
        {
            moveDirection = Vector3.zero; // Stop movement when input is released
        }

        // Apply manual gravity
        if (!isGrounded)
        {
            moveDirection.y += gravity * gravityScale;
        }
        else
        {
            moveDirection.y = rb.velocity.y; // Retain current vertical velocity if grounded
        }

        // Apply movement to the Rigidbody
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

        // Rotate the player to face the movement direction
        if (new Vector3(inputDirection.x, 0, inputDirection.z).magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(inputDirection.x, 0, inputDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
