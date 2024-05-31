using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderScript : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Animator animator;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(GoRandomPlace());
    }

    private IEnumerator GoRandomPlace() {
        float x = Random.Range(-4.5f, 4.5f), z = Random.Range(-4.5f, 4.5f);

        Debug.Log("Now pathed to " + x + " and " + z);

        agent.SetDestination(new Vector3(x, transform.position.y, z));
        animator.SetInteger("MoveSpeed", 2);

        yield return new WaitForSeconds(2f);

        // Has spider reached? If not, keep pausing the code here
        while (agent.remainingDistance > 0.1f) {
            Debug.Log("WAITING");
            yield return new WaitForFixedUpdate();
        }

            Debug.Log("Stopped " + agent.remainingDistance);


        // Spider Reached! Set to Idle and wait random seconds then do another random movement
        animator.SetInteger("MoveSpeed", 0);

        float randomWaitTime = Random.Range(3f, 7f);
        Debug.Log("Reached! Now waiting for " + randomWaitTime + " before selecting another destination");
        yield return new WaitForSeconds(randomWaitTime);

        StartCoroutine(GoRandomPlace());
    }

}