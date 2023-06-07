using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Offset from the player's position

    public float smoothSpeed = 10f; // Speed at which the camera follows the player
    public float rotationSpeed = 5f; // Speed at which the camera rotates

    private Quaternion targetRotation; // Target rotation for the camera

    void LateUpdate()
    {
        if (target == null)
        {
            return; // Exit if the target is not assigned
        }

        // Calculate the target position by adding the offset to the player's position
        Vector3 targetPosition = target.position + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Calculate the target rotation based on the player's rotation
        targetRotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);

        // Smoothly rotate the camera towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    
}
