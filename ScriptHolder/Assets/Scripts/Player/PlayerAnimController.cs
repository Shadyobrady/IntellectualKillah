using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator controller;

    private PlayerShooting shoot;
	// Use this for initialization
	void Start ()
	{
	    controller = GetComponent<Animator>();
	    shoot = GetComponentInChildren<PlayerShooting>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //CheckWeapon(shoot.currentWeapon);
	}

    private void CheckWeapon(Weapon currentWeapon)
    {
        //this script will either change the layer or the controller used which ever is easier
        //int layerIndexRequired = controller.GetLayerIndex(currentWeapon.Name+"layer");
        //controller.SetLayerWeight(layerIndexRequired);
    }
}
