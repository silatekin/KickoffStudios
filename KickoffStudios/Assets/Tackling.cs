using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : MonoBehaviour
{
    public GameObject Ball;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == Ball)
        {
            Rigidbody BallRigidbody = Ball.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * 500; 
            BallRigidbody.AddForce(force);
        }
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
