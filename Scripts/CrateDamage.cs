using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDamage : MonoBehaviour {

    public float health = 50f;
    public GameObject destroyedVersion;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
