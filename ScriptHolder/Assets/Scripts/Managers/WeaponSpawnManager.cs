﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour
{

    public GameObject Shotgun;
    public GameObject Ar;
    public GameObject SniperRifle;
    public GameObject Knife;
    public GameObject Pistol;
    public GameObject FryingPan;
    private GameObject[] WeaponSpawn;
    private List<GameObject> WeaponObj;
    // Use this for initialization
    void Start ()
    {
        
        WeaponSpawn = GameObject.FindGameObjectsWithTag("WeaponSpawns");
        WeaponObj = CreateWeaponList();
        Spawn();

 
    }


    void Spawn()
    {
        
        System.Random rand = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            int spawnPointIndex = rand.Next(WeaponSpawn.Length);
            Instantiate(WeaponObj[rand.Next(WeaponObj.Count)], WeaponSpawn[spawnPointIndex].transform.position, WeaponSpawn[spawnPointIndex].transform.rotation);
        }

    }

    private List<GameObject> CreateWeaponList()
    {   
        List<GameObject> weaplist = new List<GameObject>();
        weaplist.Add(Shotgun);
        weaplist.Add(Ar);
        weaplist.Add(SniperRifle);
        weaplist.Add(Knife);
        weaplist.Add(Pistol);
        weaplist.Add(FryingPan);
        return weaplist;
    }
}
