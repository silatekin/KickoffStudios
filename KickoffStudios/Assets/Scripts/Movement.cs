using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PhotonView pv;

    public CharacterController controller;

    public float speed = 6f;
    public float jumpForce = 10f;
    private float turnSmoothVelocity;
    private Vector3 smoothMove;
    private Quaternion smoothRotation;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;
    
    private Transform playerTransform;
    public float rotationSpeed = 5f;

    private float mouseX;
    

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        pv.ViewID = PhotonNetwork.AllocateViewID(2);
        
       
        if (!pv.IsMine)
        {
            Destroy(GetComponentInChildren<CharacterController>());
        }
        
        /*
        smoothMove = transform.position;
        smoothRotation = transform.rotation;
        */
    }



    void Update()
    {
        if (!pv.IsMine)
        {
            return;  
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 18f;
        }
        else
        {
            speed = 6f;
        }

        Move();
        ApplyGravity();
        Look();

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Jump();
        }
        
        
        /*
        else
        {
            smoothMovement();
            smoothRotate();
        }
        */
    }

    private void Jump()
    {
        velocity = Mathf.Sqrt(jumpForce * -2f * gravity * gravityMultiplier);

        Vector3 jumpVelocity = Vector3.up * velocity;
        controller.Move(jumpVelocity * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocity < 0.0f)
        {
            velocity = -1.0f;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        // Apply gravity to player's position
        Vector3 gravityVector = Vector3.up * velocity;
        controller.Move(gravityVector * Time.deltaTime);

    }

    void Look()
    {
        // Get the mouse input along the horizontal and vertical axes
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player around the Y-axis based on the mouse input
        playerTransform.Rotate(Vector3.up, mouseX * rotationSpeed);

        // Calculate the rotation angle around the X-axis based on the mouse input
        float angleX = -mouseY * rotationSpeed;

        // Apply rotation clamping to prevent the camera from rotating beyond certain angles
        Vector3 currentRotation = Camera.main.transform.localRotation.eulerAngles;
        float newAngleX = currentRotation.x + angleX;
        if (newAngleX > 180f)
        {
            newAngleX -= 360f;
            newAngleX = Mathf.Clamp(newAngleX, -60f, 20f);
            newAngleX += 360f;
        }
        else
        {
            newAngleX = Mathf.Clamp(newAngleX, -60f, 20f);
        }

        // Rotate the camera around the X-axis
        Camera.main.transform.localRotation = Quaternion.Euler(newAngleX, 0f, 0f);
    }
    


    public void Move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            controller.Move(move * speed * Time.deltaTime);
            
        }

    /*
        private void smoothMovement()
        {
            transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10f);
        }

        private void smoothRotate()
        {
            Quaternion targetRotation = Quaternion.Euler(smoothRotation.eulerAngles);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(playerTransform.position);
                stream.SendNext(playerTransform.rotation);
            }
            else if (stream.IsReading)
            {
                smoothMove = (Vector3)stream.ReceiveNext();
                smoothRotation = (Quaternion)stream.ReceiveNext();
            }
        }
        */
    }
