using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy1;
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
            if(i <= 5)
            {
                int spawnPointIndex = rand.Next(spawnPoints.Length);
                Instantiate(enemy1, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
            }
            else
            {
                int spawnPointIndex = rand.Next(spawnPoints.Length);
            Instantiate (enemy, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);
            }
        }
        
    }
}
