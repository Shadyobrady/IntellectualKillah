using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    GameObject[] spawnPoints;


    void Start ()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("AiSpawn");
        Spawn();
    }


    void Spawn ()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i <10;i++)
        {
            int spawnPointIndex = rand.Next(spawnPoints.Length);
            Instantiate (enemy, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
        }
        
    }
}
