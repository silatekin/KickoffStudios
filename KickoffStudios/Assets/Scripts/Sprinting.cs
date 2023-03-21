using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : MonoBehaviour
{
    public bool isMoving = false;
    public float movementSpeed = 5.0f; 
    public float sprintSpeed = 10f; //  sprinting speed

    private bool isSprinting = false;

    // Start is called before the first frame update
    void Start()
    {
     currentSpeed = movementSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        Input();
        PlayerMovement();
    }
    private void Input()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = movementSpeed;
        }
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        transform.Translate(x, 0, z);
    }
}








