using System.Linq;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private GameObject[] Ai;
    


    //Animator anim;


    void Awake()
    {
        //anim = GetComponent<Animator>();
    }


    void Update()
    {
        Ai = GameObject.FindGameObjectsWithTag("Ai");
        if (playerHealth.currentHealth <= 0)
        {
            PlayerDeath();
        }
        if (Ai.Length == 0)
        {
            PlayerVictory();
        }

    }

    void PlayerDeath()
    {
        
    }

    void PlayerVictory()
    {
        
    }
}
