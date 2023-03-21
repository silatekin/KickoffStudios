using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dribble : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float shootForce = 10f;

    private Rigidbody playerRb;
    private Rigidbody ballRb;
    private Vector3 ballOffset;

    private bool ballAttached;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            AttachBall(other);
        }
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical);

        RotatePlayer(moveDirection);

        if (ballAttached)
        {
            ballRb.MovePosition(transform.position - ballOffset);
        }

        MovePlayer(moveDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
        }
    }

    private void AttachBall(Collider other)
    {
        ballAttached = true;
        ballRb = other.GetComponent<Rigidbody>();
        ballRb.useGravity = false;
        ballRb.isKinematic = true;
        ballOffset = transform.position - ballRb.transform.position;
    }

    private void RotatePlayer(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        playerRb.MovePosition(playerRb.position + moveDirection * speed * Time.deltaTime);
    }

    private void ShootBall()
    {
        if (!ballAttached) return;

        Vector3 shootDirection = transform.forward;
        ballRb.isKinematic = false;
        ballRb.useGravity = true;
        ballAttached = false;
        ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
        ballRb = null;
    }
}
