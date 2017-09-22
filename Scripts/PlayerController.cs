using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    public float health;
    public float ammototal;
    public Weapon currentweapon;
    public float playerspeed;
    public List<String> weaponlist;
    public Weapon steriodenhanced;
    public float maxhealth;
    private Rigidbody2D rb;


    // Use this for initialization
    void Start () {
        weaponlistbuilder();
        currentweapon = Punch;
        maxhealth = 500;
        health = maxhealth;
        ammototal = 150;
        playerspeed = 5f;
	}

    public weaponlistbuilder()
    {
        
    }
//unfinished reads in the list of weapons from the txt file and returns a list with all weapons and then creates a weapon foreach;
    
        
    void Update () {
                if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(rb.velocity.x,playerspeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(rb.velocity.x, playerspeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(playerspeed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(-playerspeed, rb.velocity.y);
        }
    }
    //only has movement at this point

    public void newweapon(string newWeaponName)
    {
        if (currentweapon.Name == Punch)
        {
            currentweapon = ;
                //needs to prompt a anim change
            }
        else
        {
            //ui message saying e to pick up
            if (//e is pressed)
                    {
                currentweapon = ;
                //anim changes;
            }
        }
    }
    //assigns new weapon and changes the anim used

    void OnTriggerEnter3D(Collider ctrig)
    {
        //ctrig = pointcontactzone;
        if (ctrig.tag == "Health")
        {
            healthincrease();
            Destroy(ctrig.gameObject);
        }
        if (ctrig.tag == "Ammo")
        {
            ammoincrease();
            Destroy(ctrig.gameObject);
        }
        if (weaponlist.Contains(ctrig.tag))
        {
            string newweaponname = ctrig.tag;
            newweapon(newweaponname);

        }
        if (ctrig.tag == "Steriods")
        {
            onstreiods();
            Invoke("cooldown", 30);
            Invoke("returntonormal", 45);
        }
    }
    //runs the colliders for weapons,steriods,healthpacks,ammo
    private void returntonormal()
    {
        playerspeed = 5f;

    }
    //returns the players speed back to normal
    private void cooldown()
    {
        playerspeed = 3f;
       
    }
    //runs the cooldown after steriods reduces speed to 75% orginal, changes melee damage back to normal
    private void onstreiods()
    {
        if(currentweapon.Melee == true)
        {
            currentweapon.Damage = currentweapon.Damage * 2;
        }
        playerspeed = 8f;
        //ui message that states steriods active
    }
    //increase melee damage by 200% and increases speed by 50%
    private void ammoincrease()
    {
        
        System.Random ammoRand = new System.Random();
        int ammoIncreaseAmount = ammoRand.Next(1,30);
        ammototal += ammoIncreaseAmount;
        //ui message to show the increase
    }
    //this method increase the ammo total by a random amount between 1 and 30
    private void healthincrease()
    {
        
        float preincrease = health;
        float increase = 50;
        health += increase;
        if(health>maxhealth)
        {
            increase = (maxhealth - preincrease) * -1;
            health = maxhealth;
        }
        //ui message to show changes
    }
    //this method increase the health by 50 and performs a check to ensure that player cant go above the max health
}


