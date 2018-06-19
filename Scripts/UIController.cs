using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public PlayerController player;
    public WeaponSwitching weaponSwitching;

    public Text scoreText;
    public Text ammoText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "" + player.playerScore;
	}
}
