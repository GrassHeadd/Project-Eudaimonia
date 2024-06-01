using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SpiderScript : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private GameObject player;

    private bool StepCounter = false;


    [SerializeField] /*serializeField makes it show up on unity under the inspector so we can edit it directly there*/ private float minCoord = -4.5f;
    [SerializeField] private float maxCoord = 4.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(GoRandomPlace(minCoord, maxCoord, 1));
    }

    void FixedUpdate()
    {
        StartCoroutine(MovementNearPlayer());
    }
    public IEnumerator GoRandomPlace(float lower, float upper, int speed)
    {
        //generate a random max and min coordinate
        float x = UnityEngine.Random.Range(lower, upper), z = UnityEngine.Random.Range(lower, upper);

        Debug.Log("Now pathed to " + x + " and " + z);

        //set the spider to travel to a random location
        agent.SetDestination(new Vector3(x, transform.position.y, z));

        //set the movespeed
        animator.SetInteger("MoveSpeed", speed);

        yield return new WaitForSeconds(2f);

        // Has spider reached? If not, keep pausing the code here
        while (agent.remainingDistance > 0.1f)
        {
            Debug.Log("WAITING");
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Stopped " + agent.remainingDistance);


        // Spider Reached! Set to Idle and wait random seconds then do another random movement
        animator.SetInteger("MoveSpeed", 0);

        float randomWaitTime = UnityEngine.Random.Range(3f, 7f);
        Debug.Log("Reached! Now waiting for " + randomWaitTime + " before selecting another destination");
        yield return new WaitForSeconds(randomWaitTime);

        StepCounter = false;
        StartCoroutine(GoRandomPlace(lower, upper, speed));
    }

    public IEnumerator MovementNearPlayer()
    {
        if(StepCounter == true) {
            yield return new WaitForFixedUpdate();
        }

        //get location of the spider and player
        Vector3 spiderPos = transform.position;
        Vector3 playerPos = player.transform.position;
        //gets the distance between them
        float distanceBetwSpiderNPlayer = Vector3.Distance(spiderPos, playerPos);
        Debug.Log("Distance: " + distanceBetwSpiderNPlayer);
        //keep recalculating the next position if near the player and move to that point
        if (distanceBetwSpiderNPlayer < 0.5)
        {
            StartCoroutine(GoRandomPlace(minCoord, maxCoord, 2));
            StepCounter = true;
            Debug.Log("StepCounter: "  + StepCounter);
            yield return new WaitForFixedUpdate();
            
        }
        StepCounter = false;
    }
}










