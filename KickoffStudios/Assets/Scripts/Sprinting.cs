using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinting : MonoBehaviour
{
    public bool isMoving = false;
    public float movementSpeed = 5.0f; 
    public float sprintSpeed = 10f; //  sprinting speed

    private bool isSprinting = false;

  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        float speed = isSprinting ? sprintSpeed : movementSpeed;

        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.Translate(x, 0, z);
    }
}






