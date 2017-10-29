using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class AiController : MonoBehaviour {

    public float aihealth;
    public float aispeed;
    public Weapon aiweapon;
    public string state;
    public GameObject player;
    public float stopdist;
    public float sight;
    public bool wanderConstrained = false;
    public Vector2 wanderrange;
    public float wanderWaitMin;
    public float WanderWaitMax;
    public Vector2 wanderAreaCenter;
    public Vector2 target;
    public bool wandering = false;

	// Use this for initialization
	void Start () {
        aispeed = 0.05f;
        stopdist = 2f;
        sight = 6f;
        wanderAreaCenter = transform.position;
        target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        wander();
        enemycheck();
	}

    private void finderwander()
    {
        if(wandering)
        {
            if(wanderConstrained)
            {
                target = new Vector2(UnityEngine.Random.Range(wanderAreaCenter.x - wanderrange.x, wanderAreaCenter.x + wanderrange.x), UnityEngine.Random.Range(wanderAreaCenter.y + wanderrange.y, wanderAreaCenter.y - wanderrange.y));
            }
            else
            {
                target = new Vector2(UnityEngine.Random.Range(transform.position.x - wanderrange.x, transform.position.x + wanderrange.x), UnityEngine.Random.Range(transform.position.y + wanderrange.y, transform.position.y - wanderrange.y));
            }
            Invoke("finderwander", UnityEngine.Random.Range(wanderWaitMin, WanderWaitMax));
        }
    }

    private void wander()
    {
        if(!wandering)
        {
            Invoke("finderwander", UnityEngine.Random.Range(wanderWaitMin, WanderWaitMax));
            wandering = true;
        }
        target = new Vector2(UnityEngine.Random.Range(-wanderrange.x, wanderrange.x), UnityEngine.Random.Range(-wanderrange.y, wanderrange.y));
        transform.position = Vector3.MoveTowards(transform.position, target, aispeed);
    }

    private void enemycheck()
    {
       //checks if the player is within the sight range and triggers the aicombat
    }
}
