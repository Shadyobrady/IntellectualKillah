using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class PlayerController : MonoBehaviour {

    public float health;
    public float ammototal;
    public Weapon currentweapon;
    public float playerspeed;
    public Dictionary<string,Weapon> weaponlist;
    public Weapon steriodenhanced;
    public float maxhealth;
    private Rigidbody2D rb;


    // Use this for initialization
    void Start () {
        weaponlistcreate();
        weaponlist.TryGetValue("Punch", out currentweapon);
		AnimationControllerScript.weaponselect(currentweapon);
        maxhealth = 500;
        health = maxhealth;
        ammototal = 150;
        playerspeed = 5f;
	}

    public Dictionary<String,Weapon> weaponlistcreate()
    {
        weaponlist = new Dictionary<string,Weapon>();
        using (StreamReader reader = new StreamReader("Assets/files/weapon.txt"))
            while (!reader.EndOfStream)
            {
                string currentline = reader.ReadLine();
                string[] pts = currentline.Split(':');
                Weapon newweap = new Weapon(float.Parse(pts[0]), float.Parse(pts[1]), float.Parse(pts[2]), float.Parse(pts[3]), pts[4], bool.Parse(pts[5]));
                weaponlist.Add(pts[4],newweap);
            }
        return weaponlist;
    }
//unfinished reads in the list of weapons from the txt file and returns a list with all weapons and then creates a weapon foreach;
    
        
    void Update () {
		if (Input.GetKey(KeyCode.W))
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(rb.velocity.x,playerspeed);
        }
		if (Input.GetKey(KeyCode.S))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(rb.velocity.x, playerspeed);
        }
			if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(playerspeed, rb.velocity.y);
        }
			if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(-playerspeed, rb.velocity.y);
        }
            if(Input.GetKey(KeyCode.R))
        {
            reload();
        }
            if(currentweapon.Ammo == 0)
        {
            reload();
        }
    }
    //only has movement at this point

    private void reload()
    {
        AnimationControllerScript.Reload();
        Weapon tempholder;
        weaponlist.TryGetValue(currentweapon.Name, out tempholder);
        float dif = (tempholder.Ammo - currentweapon.Ammo);
        currentweapon.Ammo = tempholder.Ammo;
        ammototal -= dif;
        //Ui needs to change
    }
           public void newweapon(string newWeaponName)
    {
        if (currentweapon.Name == "Punch")
        {
            weaponlist.TryGetValue(newWeaponName, out currentweapon);
			AnimationControllerScript.Weaponselect(currentweapon);
            }
        else
        {
            //ui message saying e to pick up
			if (Input.GetKey(KeyCode.F))
                    {
                weaponlist.TryGetValue(newWeaponName, out currentweapon);
                AnimationControllerScript.Weaponselect(currentweapon);
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
        if (weaponlist.ContainsKey(ctrig.tag))
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


