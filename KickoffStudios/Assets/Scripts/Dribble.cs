using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dribble : MonoBehaviour
{
    public float speed = 5.0f;
    public float shootForce = 10.0f;
    private bool ballAttached = false;
    private Rigidbody ballRb;
    public GameObject ballLocation;    
    
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
        }

        if (Input.GetKeyDown(KeyCode.E))
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

