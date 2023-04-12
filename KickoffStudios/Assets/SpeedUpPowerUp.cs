using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedUp")]

public class SpeedUpPowerUp : PowerUpEffect
{
    public float duration;
    public float amount;
    public override IEnumerator Apply(GameObject target, GameObject self)
    {
        target.GetComponent<Movement>().speed += amount;

        self.GetComponent<MeshRenderer>().enabled = false;
        self.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(duration);
        target.GetComponent<Movement>().speed -= amount;
        
    }
}
