using UnityEngine;
using Photon.Pun;

public class playerMovement : MonoBehaviourPunCallbacks
{
    private Camera playerCamera;
    private CharacterController characterController;

    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private Vector3 moveDirection;
    private float cameraRotationX = 0f;
    private float cameraRotationLimit = 85f;

    private void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(GetComponent<CharacterController>());
        }
        else
        {
            playerCamera = GetComponentInChildren<Camera>();
            characterController = GetComponent<CharacterController>();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;

        HandleMovement();
        HandleMouseRotation();
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection.Normalize(); // Ensure constant speed regardless of direction
        moveDirection *= movementSpeed;

        // Apply gravity
        moveDirection.y += gravity * Time.deltaTime;

        // Move the character controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player character
        transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);

        // Rotate the camera vertically
        cameraRotationX -= mouseY * rotationSpeed * Time.deltaTime;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
    }

    private void HandleJump()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
