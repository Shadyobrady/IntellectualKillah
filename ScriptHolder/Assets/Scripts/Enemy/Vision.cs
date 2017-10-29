using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;

public class Vision : MonoBehaviour {
    public float sightRange;
    [Range(0, 360)]
    public float sightAngle;

    private Weapon currentWeapon;

    private EnemyAttack attakcscript;
    //Layers for determining what is a target and what is an obstacle
    public LayerMask targetLayer, obstacleLayer;

    //List of targets that the AI can see (within range, within field of view, not behind an obstacle)
    public List<GameObject> targetsInSight = new List<GameObject>();

    public GameObject closestTarget = null; 

    // Update is called once per frame
    void Awake()
    {
        attakcscript = GetComponent<EnemyAttack>();
        currentWeapon = attakcscript.currentweapon;
        sightRange = currentWeapon.Range;
        
    }
    void Update()
    {
        FindTargets();
        Debug.DrawRay(transform.position, transform.forward * sightRange);
        closestTarget = targetsInSight.First();
    }

    void FindTargets()
    {
        //Clear the List of targets to avoid duplicates
        targetsInSight.Clear();

        //Find all the targets within the AI's sight range
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, sightRange, targetLayer);

        //Loop through the array of targets
        for (int i = 0; i < targetsInRange.Length; i++)
        {
            //Get a references to each of the targets transform component
            Transform target = targetsInRange[i].transform;

            //Calculate the direction from the AI to the target
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //Check if the target is within the AI's field of view
            if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
            {
                //Calculate the distance between the AI and the target
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //Use a raycast to check if there is an obstacle between the AI and the target
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    //Do whatever you want, you've found your target
                    Debug.DrawLine(transform.position, target.position, Color.green);
                    GameObject targetGameObject = target.gameObject;
                    targetsInSight.Add(targetGameObject);
                    }
            }
        }
    }
}