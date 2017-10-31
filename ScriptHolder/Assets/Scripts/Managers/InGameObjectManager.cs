using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class InGameObjectManager : MonoBehaviour
{

    public GameObject Ammo;
    public GameObject Health;
    public GameObject Steriods;
    GameObject[] ObjectspawnPoints;
    private Dictionary<int, GameObject> objdic;


    void Start()
    {
        ObjectspawnPoints = GameObject.FindGameObjectsWithTag("ObjectSpawn");
        Spawn();
    }

    private Dictionary<int, GameObject> objectSorter()
    {
        Dictionary<int,GameObject> dic = new Dictionary<int, GameObject>();
        dic.Add(1,Ammo);
        dic.Add(2,Health);
        dic.Add(3,Steriods);
        return dic;
    }
    void Spawn()
    {
        
        System.Random rand = new System.Random();
        objdic = objectSorter();
        GameObject spawnObject;
        int next = rand.Next(objdic.Count);
        objdic.TryGetValue(next, out spawnObject);
        for (int i = 0; i < 5; i++)
        {
            int spawnPointIndex = rand.Next(ObjectspawnPoints.Length);
            Instantiate(spawnObject, ObjectspawnPoints[spawnPointIndex].transform.position,
                ObjectspawnPoints[spawnPointIndex].transform.rotation);
        }

    }
}