using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    
    public HeavyScript heavy;
    public SniperScript sniper;
    public RifleScript rifle;
    public UIController UI;

    public int selectedWeapon = 0;
    public Animator anim;

    public bool isSwitching;

	// Use this for initialization
	void Start () {
        SelectWeapon();
	}
	
	// Update is called once per frame
	void Update () {
        int previousSelectedWeapon = selectedWeapon;

		if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                UI.ammoText.text = "Ammo: 12";
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            rifle.maxAmmo = 60;
            UI.ammoText.text = "Ammo: " + rifle.maxAmmo;
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            heavy.maxAmmo = 8;
            UI.ammoText.text = "Ammo: " + heavy.maxAmmo;
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            sniper.maxAmmo = 1;
            UI.ammoText.text = "Ammo: " + sniper.maxAmmo;
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
                i++;
        }
    }
}
