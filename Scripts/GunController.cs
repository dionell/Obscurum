using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public int damage = 10;
    public float range = 100f;
    private float impactForce = 30f;
    public float fireRate = 15f;

    public AudioSource fireWeapon;

    public int maxCurrentAmmo;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private bool isShooting = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    private float nextTimeToFire = 0f;

    public Animator anim;
    public PlayerController player;

    void Start()
    {
            currentAmmo = maxCurrentAmmo;
    }
    void OnEnable() //called every single time
    {
        isReloading = false;
        isShooting = false;
        anim.SetBool("Reloading", false);
        anim.SetBool("isShooting", false);
    }


    // Update is called once per frame
    void Update () {

        if (isReloading)
        {
            return;
        }

        if (isShooting)
        {
            return;
        }

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
                    anim.Play("WeaponShoot");
                    fireWeapon.Play();
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
        }
	}

    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - .25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxCurrentAmmo;
        isReloading = false;
    }  

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            CrateDamage crate = hit.transform.GetComponent<CrateDamage>();
            MonsterController monster = hit.transform.GetComponent<MonsterController>();

            if (crate != null)
            {
                crate.TakeDamage(damage);
            }
            if (monster != null)
            {
                player.playerScore += 10;
                monster.TakeDamage(damage);
                GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2f);
            }
        }
    }
}