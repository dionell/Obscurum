using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour {

    private float nextTimeToFire = 1f;
    public float fireRate = 3f;

    public Animator anim;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            anim.Play("WeaponShoot");
            nextTimeToFire = Time.time + 1f / fireRate;
            Debug.Log("Firing Weapon");
        }

    }
}
