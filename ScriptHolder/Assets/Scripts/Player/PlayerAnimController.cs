using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator Controller;

    void Awake()
    {
        Controller = GetComponent<Animator>();
    }

    public void PlayerDeath()
    {
        Debug.Log("Set Player Trigger to Dead");
        Controller.SetTrigger("isDead");
    }

    public void Animating(float Horizon, float Vert)
    {
        bool walking = Horizon != 0f || Vert != 0f;
        Controller.SetBool("isWalking", walking);
    }

    public void ReloadAnim()
	{
	    Debug.Log("Reload bool set to true");
	        Controller.SetBool("isReloading",true);
	    }

    public void ReloadAnimComplete()
    {
        Debug.Log("Reload bool reset");
        Controller.SetBool("isReloading",false);
    }

    public void Attack()
    {
        Debug.Log("Attack Anim Bool Changed");
        Controller.SetBool("Attacking",true);
    }

    public void ResetAttack()
    {
        Debug.Log("Reset Attack Bool Begun");
        Controller.SetBool("Attacking",false);
    }

    void ResetWeaponAnimBools()
    {
        Debug.Log("Weapon Bools Reset Begun");
        Controller.SetBool("Ar",false);
        Controller.SetBool("Shotgun", false);
        Controller.SetBool("SniperRifle", false);
        Controller.SetBool("Knife", false);
        Controller.SetBool("Pistol", false);
        Controller.SetBool("FryingPan", false);
        Debug.Log("Weapon Bools Reset Complete");
    }

    public void CheckWeapon(Weapon currentWeapon)
    {
        Debug.Log("Running Checking");
        ResetWeaponAnimBools();
        if (currentWeapon.Name != "Punch")
        {
            Controller.SetBool(currentWeapon.Name, true);
        }
        Debug.Log("Check Weapon Complete");
    }
}
