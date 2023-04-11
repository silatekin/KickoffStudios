using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedUp")]

public class SpeedUpPowerUp : PowerUpEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Movement>().speed += amount;
    }
}
