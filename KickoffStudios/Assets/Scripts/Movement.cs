using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PhotonView pv;
    public CharacterController controller;
    //public Rigidbody rb;
    public float speed = 6f;
    public float turnSmoothTime = 0.6f;
    public float jumpForce = 10f;
    private float turnSmoothVelocity;
    private Vector3 smoothMove;
    private Quaternion smoothRotation;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;
    

    /*
    public PhotonView pv;
    public CharacterController controller;
    public CinemachineVirtualCamera cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.6f;
    private float turnSmoothVelocity;
    private Vector3 moveDirection = Vector3.zero;

    private Vector3 smoothMove;
    private Quaternion smoothRotation;
    */

    // Update is called once per frame


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    

    void Update()
    {
        if (pv.IsMine)
        {
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

            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                Jump();
            }
        }
        else
        {
            //virtualCamera.gameObject.SetActive(false);
            smoothMovement();
            smoothRotate();
        }
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
    
    
    public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);

        //rb.AddForce(Vector3.down * 20f, ForceMode.Acceleration);
    }
    

    

    /*public void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
    }
    */
    
    private void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void smoothRotate()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            
            float t = Time.deltaTime * 2f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.Clamp01(t));
        }
        
        //transform.rotation = Quaternion.Lerp(transform.rotation, smoothRotation, 720.0f * Time.deltaTime);
    }
    
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }else if (stream.IsReading)
        {
            smoothMove = (Vector3) stream.ReceiveNext();
            smoothRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
