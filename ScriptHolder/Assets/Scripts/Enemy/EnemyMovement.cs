using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;
    private GameObject targetGameObject;
    private Vision visionScript;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
        visionScript = GetComponent<Vision>();
    }


    void Update ()
    {
        if (visionScript.closestTarget != null)
        {
            targetGameObject = visionScript.closestTarget;
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(targetGameObject.transform.position);
            }
            else
            {
                nav.enabled = false;
            }
        }
        else if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.transform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
