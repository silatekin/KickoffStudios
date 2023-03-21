using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : MonoBehaviour
{
    //seperated the definitions
    [SerializedField] private GameObject Ball;
    [SerializedField] private float tackleForce = 500f;

    public GameObject Ball;
    //collision
    void OnCollisionEnter(Collision collision)
    {
        if(IsCollidingWithBall(collision))
        {
            Rigidbody BallRigidbody = Ball.GetComponent<Rigidbody>();
        }
    }
    //isolated the collision
    private bool IsCollidingWithBall(Collision collision)
    {
        return collision.gameObject == Ball;
    }
    //isolated force calculation and application
    private void ApplyTackleForce()
    {
        Vector3 force = transform.forward * tackleForce;
        BallRigidbody.AddForce(force, ForceMode.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
