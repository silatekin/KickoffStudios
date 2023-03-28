using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private float health = 100f;
    private float goal = 20f;
    public Vector3 startingPosition;
    public Text healthBar;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalPost"))
        {
            transform.position = startingPosition;
            health -= goal;
            healthBar.text = health.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
