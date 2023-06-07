using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float moveDistance = 5f; 

    private Vector3 startPos; 
    private float moveDirection = 1f;  

    void Start()
    {
        startPos = transform.position; 
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, 0, 0);
        
        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            moveDirection *= -1f;
        }
    }
}
