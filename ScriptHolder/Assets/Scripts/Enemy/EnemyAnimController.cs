using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{

    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    public void isDead()
    {
        enemyAnimator.SetTrigger("isDead");
    }

    public void Attack()
    {
        enemyAnimator.SetBool("Attacking",true);
    }

    public void Walking()
    {
        enemyAnimator.SetBool("Walking",true);
    }
}
