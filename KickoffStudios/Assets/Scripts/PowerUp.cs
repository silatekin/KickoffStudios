using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;

    void OnTriggerEnter(Collider collision)
    {
        StartCoroutine(powerUpEffect.Apply(collision.gameObject, gameObject));

    }
}
