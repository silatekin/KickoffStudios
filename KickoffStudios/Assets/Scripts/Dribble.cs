using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dribble : MonoBehaviour
{
    public float speed = 5.0f;
    public float shootForce = 10.0f;

    private Rigidbody playerRb;
    private bool ballAttached = false;
    private Rigidbody ballRb;
    private Vector3 ballOffset;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballAttached = true;
            ballRb = other.GetComponent<Rigidbody>();
            ballRb.useGravity = false;
            ballRb.isKinematic = true;
            ballOffset = transform.position - ballRb.transform.position;
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        if (ballAttached)
        {
            ballRb.MovePosition(transform.position - ballOffset);
        }

        playerRb.MovePosition(playerRb.position + moveDirection * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ballAttached)
            {
                Vector3 shootDirection = transform.forward;
                ballRb.isKinematic = false;
                ballRb.useGravity = true;
                ballAttached = false;
                ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
                ballRb = null;
            }
        }
    }
}

