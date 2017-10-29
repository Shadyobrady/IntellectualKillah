using Assets.Scripts;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    Vector3 movement;
    Animator Anim;
    Rigidbody rb;
    int floorMask;
    float camRayLength = 100f;
    Collider pointcontactzone;
    Dictionary<string, Weapon> WeapDic = createWeapdic();
    PlayerShooting shoot;
    PlayerHealth health;
    List<string> obstackeList;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Ground");
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        pointcontactzone = GetComponent<Collider>();
        shoot = GetComponent<PlayerShooting>();
        health = GetComponent<PlayerHealth>();

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
        Animating(Horz, Vert);

    }

    void OnTriggerEnter(Collider ctrig)
    {
        obstackeList = obsList();
        ctrig = pointcontactzone;
        if (ctrig.tag == "Health")
        {
            health.healthincrease();
            Destroy(ctrig.gameObject);
        }
        if (obstackeList.Contains(ctrig.tag)) ;
        {
            movement = Vector3.zero;
        }
        if (ctrig.tag == "Ammo")
        {
            shoot.ammoincrease();
            Destroy(ctrig.gameObject);
        }
        if (WeapDic.ContainsKey(ctrig.tag))
        {
            string newweaponname = ctrig.tag;
            shoot.NewWeapon(newweaponname);

        }
        if (ctrig.tag == "Steriods")
        {
            onstreiods();
            Invoke("cooldown", 30);
            Invoke("returntonormal", 45);
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

    void Animating(float Horizon,float Vert)
    {
        bool walking = Horizon != 0f || Vert != 0f;
        Anim.SetBool("isWalking", walking);

   }

    private void returntonormal()
    {
        speed = 5f;

    }
    //returns the players speed back to normal
    private void cooldown()
    {
        speed = 3f;
        shoot.Resetflashputext();
    }

    //runs the cooldown after steriods reduces speed to 75% orginal, changes melee damage back to normal
    private void onstreiods()
    {
        if (shoot.currentWeapon.Melee == true)
        {
            shoot.currentWeapon.Damage = shoot.currentWeapon.Damage * 2;
        }
        speed = 8f;
        shoot.flashuptext.text = "Steriods Acivated";
    }


}
