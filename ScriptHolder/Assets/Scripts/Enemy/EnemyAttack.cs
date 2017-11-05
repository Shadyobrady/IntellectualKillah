using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using NUnit.Framework.Api;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public Weapon CurrentWeapon;
    private EnemyAnimController EnemyAnimatorScript;
    GameObject player;
    PlayerHealth playerHealth;
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
    public List<GameObject> targetsInSight = new List<GameObject>();
    public GameObject closestTarget = null;
    GameObject attackTarget;
    private GameObject thisAi;
    private Collider[] targetsInRange;

    void Awake ()
    {
        WeaponDic = createWeapDic();
        string weaponname ="";
        weaponname = randomWeapon(weaponname);
        WeaponDic.TryGetValue(weaponname, out CurrentWeapon);
        EnemyAnimatorScript = GetComponentInChildren<EnemyAnimController>();
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        shootableMask = LayerMask.GetMask("Shootable");
        wallMask = LayerMask.GetMask("Wall");
        thisAi = gameObject;
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
        Debug.Log("TargetsinSight cleared");
        targetsInRange = Physics.OverlapSphere(transform.position, sightRange, shootableMask);
        Debug.Log("All Shootablemask objects added to collider array");
        Debug.Log(targetsInRange.Length);
        foreach (Collider i in targetsInRange)
        {
            Debug.Log(i.gameObject.tag + " is currently going through Vision Check");
            if (i.gameObject == gameObject)
            {
                Debug.Log("Object identified as this object");
                continue;
            }
            Debug.Log(i.tag + "check begun");
            Transform target = i.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Debug.Log(i.tag + "direction located");
            if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
            {
                Debug.Log("Angle Check Completed");
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                Debug.Log("distance to target calculated");
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, wallMask))
                {
                    Debug.Log("Check for walls in the way completed");
                    GameObject targetGameObject = target.gameObject;
                    if (targetGameObject.tag == "Player")
                    {
                        targetsInSight.Add(targetGameObject);
                        Debug.Log("Player Located for targets List");
                    }
                    if (targetGameObject.tag == "AI")
                    {
                        targetsInSight.Add(targetGameObject);
                        Debug.Log("Ai Located for targets List");
                    }
                }
            }
            else
            {
                continue;
            }
        }
        for(int i = 1; i>= targetsInSight.Count; i++)
        {
            GameObject a = targetsInSight[i];
            float distance = Vector3.Distance(transform.position, a.transform.position);
            if (i == 1)
            {
                attackTarget = a;
            }
            if (distance < Vector3.Distance(transform.position, attackTarget.transform.position))
            {
                attackTarget = a;
            }
        }
        Debug.Log("Ai has set " + attackTarget.tag + "as target");
        Attack(attackTarget);
    }

    void Attack(GameObject targetGameObject)
    {
        Debug.Log("Attack method called");
        if (targetGameObject.tag == "Ai")
        {
            EnemyHealth targetHealth = targetGameObject.GetComponent<EnemyHealth>();
            System.Random attackvariable = new System.Random();
            int hitchance = attackvariable.Next(100);
            Debug.Log("Hit Chance Calculated");
            if (Physics.Raycast(shootRay, out shootHit, CurrentWeapon.Range, shootableMask))
                {
                if (hitchance < 50)
                {
                    if (targetHealth != null)
                    {
                        targetHealth.TakeDamage((int)CurrentWeapon.Damage);
                    }
                    gunLine.SetPosition(1, shootHit.point);
                }
            }
        }
        if (targetGameObject.tag == "Player")
        {
            System.Random attackvariable = new System.Random();
            int hitchance = attackvariable.Next(100);
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

        if(timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
        {
            Debug.Log("begining scan Range");
            ScanRange();
        }

        if(playerHealth.currentHealth <= 0)
        {
            EnemyAnimatorScript.isDead();
        }
    }



}
