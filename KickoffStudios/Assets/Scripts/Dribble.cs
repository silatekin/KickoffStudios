using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dribble : MonoBehaviour
{
    public float speed = 5.0f;
    public float shootForce = 10.0f;

    public GameObject ballLocation;    
    private bool ballAttached = false;
    private Rigidbody ballRb;
    private Vector3 ballOffset;

   
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
        if (ballAttached)
        {
            ballRb.MovePosition(ballLocation.transform.position);
           // ballRb.MovePosition(transform.position - ballOffset);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ballAttached)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector3 shootDirection = transform.forward;
        ballRb.isKinematic = false;
        ballRb.useGravity = true;
        ballAttached = false;
        ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
        ballRb = null;
    }
}

