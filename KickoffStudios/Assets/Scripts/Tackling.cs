using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : MonoBehaviour
{
    //seperated the definitions
    [SerializeField] private float tackleForce = 1000f;
    [SerializeField] private string tackleInput = "z";

    /*
    private void Awake()
    {
        ballRigidbody = ball.GetComponent<Rigidbody>();
    }
    */

    //collision
    
    private void OnCollisionEnter(Collision collision)
    {
        if(IsCollidingWithOpponent(collision) && IsButtonPressed())
        {
            ApplyTackleForce(collision);
        }
    }

    //isolated the collision
    private bool IsCollidingWithOpponent(Collision collision)
    {
        return (collision.gameObject.CompareTag("Opponent"));
    }
    
    //isolated force calculation and application
    private void ApplyTackleForce(Collision collision)
    {
        //calculate the force vector from opponent to the player
        Vector3 forceDirection = transform.position - collision.gameObject.transform.position;
        forceDirection.Normalize();
        Vector3 force = forceDirection * tackleForce;

        //Apply force to opponent
        Rigidbody opponentRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        opponentRigidbody.AddForce(force, ForceMode.Impulse);

    }
    private bool IsButtonPressed()
    {
        return Input.GetButtonDown(tackleInput);
    }
    //not used
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
       // ApplyTackleForce(IsButtonPressed() && IsCollidingWithOpponent());
    }
}
