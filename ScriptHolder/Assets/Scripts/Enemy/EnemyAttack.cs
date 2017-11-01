using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public Weapon CurrentWeapon;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    PlayerShooting shoot;
    EnemyHealth enemyHealth;
    bool TargetInRange;
    float timer;
    Dictionary<string, Weapon> WeaponDic;
    Ray shootRay;
    RaycastHit shootHit;
    int wallMask;
    int shootableMask;
    LineRenderer gunLine;
    public float sightRange;
    [Range(0, 360)]
    public float sightAngle;
    List<string> obstaList = new List<string>();
    public List<GameObject> targetsInSight = new List<GameObject>();
    public GameObject closestTarget = null;

    void Awake ()
    {
        WeaponDic = createWeapDic();
        string weaponname ="";
        weaponname = randomWeapon(weaponname);
        WeaponDic.TryGetValue(weaponname, out CurrentWeapon);
        player = GameObject.FindWithTag("Player");
        shoot = player.GetComponent<PlayerShooting>();
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        shootableMask = LayerMask.GetMask("Shootable");
        wallMask = LayerMask.GetMask("Wall");
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
        targetsInSight.Clear();
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, sightRange, shootableMask);
        for (int i = 0; i < targetsInRange.Length; i++)
        {
            Transform target = targetsInRange[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position,directionToTarget, distanceToTarget,wallMask))
                {
                   
                    Debug.DrawLine(transform.position, target.position, Color.green);
                    GameObject targetGameObject = target.gameObject;
                    if(targetGameObject.tag == "Player")
                    {targetsInSight.Add(targetGameObject); }
                    if (targetGameObject.tag == "AI")
                    { targetsInSight.Add(targetGameObject); }

                }
            }
        }
        GameObject attackTarget = targetsInSight.First();
        Attack(attackTarget);
        

    }

    void Attack(GameObject targetGameObject)
    {
        if (targetGameObject.tag == "Ai")
        {
            System.Random attackvariable = new System.Random();
            int hitchance = attackvariable.Next(1, 100);
            if (Physics.Raycast(shootRay, out shootHit, CurrentWeapon.Range, shootableMask))
                {
                if (hitchance < 50)
                {
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage((int)CurrentWeapon.Damage);
                    }
                    gunLine.SetPosition(1, shootHit.point);
                }
            }
        }
        if (targetGameObject.tag == "Player")
        {
            System.Random attackvariable = new System.Random();
            int hitchance = attackvariable.Next(1, 100);
            if (Physics.Raycast(shootRay, out shootHit, CurrentWeapon.Range, shootableMask))
            {
                if (hitchance<60)
                {
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage((int) CurrentWeapon.Damage);
                }
                gunLine.SetPosition(1, shootHit.point);
            } }
            
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
