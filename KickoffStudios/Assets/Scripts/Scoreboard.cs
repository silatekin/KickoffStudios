using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Scoreboard : MonoBehaviour
{
    private float health = 100f;
    private float goal = 15f;
    public Transform startingPosition;
    public Text healthBar; 
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalPostM"))
        {
            transform.position = startingPosition.position;
            rb.velocity = Vector3.zero;
            health -= goal;
            health = Mathf.Clamp(health, 0.0f, 100.0f);
            healthBar.text = health.ToString();
        }else if (other.CompareTag("GoalPostW"))
        {
            transform.position = startingPosition.position;
            rb.velocity = Vector3.zero;
            health += goal;
            health = Mathf.Clamp(health, 0.0f, 100.0f);
            healthBar.text = health.ToString();
        }
    }
   
}
