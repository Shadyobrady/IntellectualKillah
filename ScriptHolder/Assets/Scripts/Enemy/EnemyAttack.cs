using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using System;
using System.IO;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public Weapon currentweapon;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    PlayerShooting shoot;
    EnemyHealth enemyHealth;
    bool TargetInRange;
    float timer;
    Dictionary<string, Weapon> WeaponDic;
    Vision visionScript;

    void Awake ()
    {
        WeaponDic = createWeapDic();
        string weaponname ="";
        weaponname = randomWeapon(weaponname);
        WeaponDic.TryGetValue(weaponname, out currentweapon);
        player = GameObject.FindWithTag("Player");
        shoot = player.GetComponent<PlayerShooting>();
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        visionScript = GetComponent<Vision>();
    }

    private Dictionary<string, Weapon> createWeapDic()
    {
        Dictionary<string, Weapon> newdic = new Dictionary<string, Weapon>();
        using (StreamReader read = new StreamReader("Assets/files/weapons.txt"))
        {
            while (!read.EndOfStream)
            {
                string newinput = read.ReadLine();
                string[] pts = newinput.Split('/');
                string weaponName = pts[0];
                float damage = float.Parse(pts[1]);
                float ammo = float.Parse(pts[2]);
                float range = float.Parse(pts[3]);
                bool melee = bool.Parse(pts[4]);
                float fireRate = float.Parse(pts[5]);
                Weapon ab = new Weapon(damage, ammo, range, weaponName, melee, fireRate);
                newdic.Add(weaponName, ab);

            }
        }
        return newdic;
    }

    private string randomWeapon(string WeaponName)
    {
        string weap = WeaponName;
        System.Random rand = new System.Random();
        int rando = rand.Next(3);
        if(rando == 1)
        {
            weap = "Punch";

        }
        else if (rando == 2)
        {
            weap = "Shotgun";
        }
        else if(rando == 3)
        {
            weap = "Pistol";
        }
        return weap;
    }

    void ScanRange()
    {
        GameObject target = visionScript.closestTarget;
        if(target.CompareTag("Player"))
        {
            TargetInRange = true;
            timer = 0f;
            if (playerHealth.currentHealth > 0)
            {
                float damagetotal = currentweapon.Damage;
                playerHealth.TakeDamage(damagetotal);
            }
        }
        if (target.CompareTag("AI"))
        {
            TargetInRange = true;
            EnemyHealth targetHealth = target.GetComponent<EnemyHealth>();
            timer = 0f;
            if (targetHealth.currentHealth > 0)
            {
                float damagetotal = currentweapon.Damage;
                targetHealth.TakeDamage(damagetotal);
            }
        }

    }





    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && TargetInRange && enemyHealth.currentHealth > 0)
        {
            ScanRange();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("isDead");
        }
    }



}
