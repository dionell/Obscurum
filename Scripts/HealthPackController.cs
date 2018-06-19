using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour {

    PlayerController player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            player.currentHealth = player.maxHealth;
            player.healthSlider.value = player.currentHealth;
            Debug.Log("You picked up a shit");
            Destroy(gameObject);
        }
    }

}
