using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : MonoBehaviour
{
    //seperated the definitions
    [SerializedField] private GameObject ball;
    [SerializedField] private float tackleForce = 500f;

    //seperated rigidbody
    private Rigidbody ballRigidbody;
    
    //called only once at the beginning
    private void Awake()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }

    //collision
    private void OnCollisionEnter(Collision collision)
    {
        if(IsCollidingWithBall(collision))
        {
            ApplyTackleForce();
        }
    }
    //isolated the collision
    private bool IsCollidingWithBall(Collision collision)
    {
        return collision.gameObject == ball;
    }
    
    //isolated force calculation and application
    private void ApplyTackleForce()
    {
        Vector3 force = transform.forward * tackleForce;
        ballRigidbody.AddForce(force, ForceMode.Impulse);
    }
    //not used
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
