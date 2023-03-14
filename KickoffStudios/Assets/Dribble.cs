using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dribble : MonoBehaviour
{
    public float dribbleForce = 10f;
    private Rigidbody playerRb;
    private Rigidbody ballRb;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        ballRb = GameObject.Find("Ball").GetComponent<Rigidbody>();
    }void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // The player is close enough to the ball, so enable dribbling
            ballRb.isKinematic = true;
        }
    }void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // The player is too far from the ball, so disable dribbling
            ballRb.isKinematic = false;
        }
    }void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Apply a force to the ball in the direction that the player is facing
            ballRb.AddForce(playerRb.transform.forward * dribbleForce);
        }
    }
}
