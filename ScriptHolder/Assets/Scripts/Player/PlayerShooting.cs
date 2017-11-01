using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Weapon currentWeapon;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    int wallMask;

    //ParticleSystem gunParticles;
    LineRenderer gunLine;
    //AudioSource gunAudio;
    float effectsDisplayTime = 0.2f;
    Dictionary<string,Weapon> WeapDic = createWeapdic();
    float ammototal;
    Text ammotext;
    public Text flashuptext;

    public static Dictionary<string, Weapon> createWeapdic()
    {
        Dictionary<string, Weapon> newdic = new Dictionary<string, Weapon>();
        using (StreamReader read = new StreamReader("Assets/files/weapons.txt"))
        {
            while(!read.EndOfStream)
            {
                string newinput = read.ReadLine();
                string[] pts = newinput.Split('/');
                string weaponName = pts[0];
                float damage = float.Parse(pts[1]);
                float ammo = float.Parse(pts[2]);
                float range = float.Parse(pts[3]);
                bool melee = bool.Parse(pts[4]);
                double fireRate = Single.Parse(pts[5]);
                
                Weapon ab = new Weapon(damage, ammo, range, weaponName, melee, fireRate);
                newdic.Add(weaponName, ab);

            }
        }
        return newdic;
    }

 
    void Awake ()
    {
        WeapDic.TryGetValue("Shotgun", out currentWeapon);
        shootableMask = LayerMask.GetMask ("Shootable");
        wallMask = LayerMask.GetMask("Wall");
        //gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        //gunAudio = GetComponent<AudioSource> ();
        ammotext = GameObject.FindWithTag("ammotext").GetComponent<Text>();
        flashuptext = GameObject.FindWithTag("flashtxt").GetComponent<Text>();
        Resetflashputext();
        ammototal = 100;
    }
    
    void Update ()
    {
        timer += Time.deltaTime;
        ammotext.text = "Ammo:" + currentWeapon.Mag + "/" +ammototal;
        if (Input.GetButton ("Fire1") && timer >= currentWeapon.FireRate && Time.timeScale != 0 && currentWeapon.Mag > 0 )
        {
            Shoot ();
        }
        if(timer >= currentWeapon.FireRate * effectsDisplayTime)
        {
            DisableEffects ();
        }
        if (Input.GetKey(KeyCode.R))
        {
            reload();
        }
        if (currentWeapon.Mag == 0)
        {
            reload();
        }
    }

    public void reload()
    {
        //AnimationControllerScript.Reload();
        if (ammototal == 0) return;
        float dif = currentWeapon.Ammo - currentWeapon.Mag;
        if (ammototal - dif < 0)
        {
            currentWeapon.Mag += ammototal;
            ammototal = 0;
            ammotext.text = "Ammo:" + currentWeapon.Mag + "/" + ammototal;
        }
        else
        {
            currentWeapon.Mag = currentWeapon.Ammo;
            ammototal -= dif;
            ammotext.text = "Ammo:" + currentWeapon.Mag + "/" +ammototal; 
        }
       
    }

    public void DisableEffects ()
    {
        gunLine.enabled = false;        
    }


    void Shoot ()
    {
        timer = 0f;
        //gunAudio.Play ();          
        //gunParticles.Stop ();
        //gunParticles.Play ();
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        
                if (currentWeapon.Melee == false)
                {
                    currentWeapon.Mag -= 1;
                    ammotext.text = "Ammo:" + currentWeapon.Mag + "/" + ammototal;
                }
                if(Physics.Raycast(shootRay, out shootHit, currentWeapon.Range, shootableMask))
                {
                    EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage((int) currentWeapon.Damage, shootHit.point);
                        }
                        gunLine.SetPosition(1, shootHit.point);
                }
                if (Physics.Raycast(shootRay, out shootHit, currentWeapon.Range, wallMask))
                {
                    gunLine.SetPosition(1, shootHit.point);
                }
        else
                    {
                        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * currentWeapon.Range);
                    }
                
            }
    
      
 

    public void NewWeapon(string newWeaponName)
    {
        
        WeapDic.TryGetValue(newWeaponName, out currentWeapon);
        flashuptext.text = newWeaponName + " equipped";
        Invoke("Resetflashputext",5);
        //AnimationControllerScript.Weaponselect(currentweapon);

    }
    //assigns new weapon and changes the anim used


    public void Resetflashputext()
    {
        flashuptext.text = "";
    }

  
    public void ammoincrease()
    {
        Debug.Log("Ammo increase begun");
        System.Random ammoRand = new System.Random();
        int ammoIncreaseAmount = ammoRand.Next(30);
        ammototal = ammototal + ammoIncreaseAmount;
        ammotext.text = "Ammo:" + currentWeapon.Mag + "/" + ammototal;
        flashuptext.text = "+" + ammoIncreaseAmount + " Ammo";
        Debug.Log("Ammo increase finished");
    }
    //this method increase the ammo total by a random amount between 1 and 30
 
}
