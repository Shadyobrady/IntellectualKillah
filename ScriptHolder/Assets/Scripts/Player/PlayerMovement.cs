using Assets.Scripts;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    Vector3 movement;
    private PlayerAnimController Anim;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;
    Dictionary<string, Weapon> WeapDic = createWeapdic();
    PlayerShooting shoot;
    PlayerHealth health;
    List<string> obstackeList;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Ground");
        Anim = GetComponentInChildren<PlayerAnimController>();
        rb = GetComponent<Rigidbody>();
        shoot = GetComponentInChildren<PlayerShooting>();
        health = GetComponent<PlayerHealth>();
        speed = 30;
    }

    private static List<string> obsList()
    {
        List<string> obslist = new List<string>();
        using (StreamReader reader = new StreamReader("Assets/files/Obstacles.txt"))
        {
            while (!reader.EndOfStream)
            {
                string input = reader.ReadLine();
                obslist.Add(input);
            }
        }
        return obslist;
    }
    public static Dictionary<string, Weapon> createWeapdic()
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


    void FixedUpdate()
    {
        float Horz = Input.GetAxisRaw("Horizontal");
        float Vert = Input.GetAxisRaw("Vertical");
        Move(Horz, Vert);
        Turning();
        Anim.Animating(Horz, Vert);
    }

    void OnCollisionEnter(Collision ctrig)
    {
        obstackeList = obsList();
        string objectTag = ctrig.gameObject.tag;
        if (objectTag == "Health")
        {
            Debug.Log(objectTag);
            health.healthincrease();
            DestroyObject(ctrig.gameObject);
        }
        if (obstackeList.Contains(objectTag))
        {
            Debug.Log(objectTag);
            movement = Vector3.zero;
        }
        if (objectTag == "Ammo")
        {
            shoot.ammoincrease();
            Debug.Log(objectTag + "picked up");
            DestroyObject(ctrig.gameObject);
        }
        if (WeapDic.ContainsKey(objectTag))
        {
            shoot.NewWeapon(objectTag);
            DestroyObject(ctrig.gameObject);

        }
        if (objectTag == "Steriods")
        {
            Debug.Log(objectTag);
            onstreiods();
            DestroyObject(ctrig.gameObject);
        }
    }

    void Move(float Horziontal,float Vertical)
    {
        movement.Set(Horziontal, 0f, Vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit camRayHit;
        if(Physics.Raycast(camRay,out camRayHit,camRayLength,floorMask) )
        {
            Vector3 playerToMouse = camRayHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRot = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRot);
        }
    }


    private void returntonormal()
    {
        speed = 30;

    }
    //returns the players speed back to normal
    private void cooldown()
    {
        speed = 10;
        shoot.Resetflashputext();
        Invoke("returntonormal", 10);
    }

    //runs the cooldown after steriods reduces speed to 75% orginal, changes melee damage back to normal
    private void onstreiods()
    {
        if (shoot.currentWeapon.Melee == true)
        {
            shoot.currentWeapon.Damage = shoot.currentWeapon.Damage * 2;
        }
        speed = 100;
        shoot.flashuptext.text = "Steriods Acivated";
        Invoke("cooldown", 10);
    }


}
