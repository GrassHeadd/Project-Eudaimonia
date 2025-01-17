using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SnakeScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private GameObject player; 
    [SerializeField] private GameObject escapeObj;
    //------------------Coords---------------------
    [SerializeField]  private float minCoord = 0f; 
    [SerializeField] private float maxCoord = 100f;
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 7f;
    private Coroutine walkTask = null, escapeTask;
    
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        walkTask = StartCoroutine(GoRandomPlace(minCoord, maxCoord, 1));
        
    }

    public IEnumerator GoRandomPlace(float lower, float upper, int speed) {
        //Debug.Log("Walking normally === ");
        //generate a random max and min coordinate
        float x = UnityEngine.Random.Range(lower, upper), z = UnityEngine.Random.Range(lower, upper);

        //Debug.Log("Now pathed to " + x + " and " + z);

        Vector3 newRandomPos = new Vector3(x, transform.position.y, z);
        agent.SetDestination(newRandomPos);
        escapeObj.transform.position = newRandomPos;

        //set the animation move speed
        animator.SetBool("isRunning", true);

        //waiting for calculations of the new coordinates so it starts walking
        yield return new WaitForSeconds(2f);

        // Has snake reached? If not, keep pausing the code here
        while (agent.remainingDistance > 0.1f)
        {
            //Debug.Log("WAITING");
            yield return new WaitForFixedUpdate();
        }

        //Debug.Log("Stopped " + agent.remainingDistance);
        // Snake Reached! Set to Idle and wait random seconds then do another random movement
        animator.SetBool("isRunning", false);
        float randomWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        //Debug.Log("Reached! Now waiting for " + randomWaitTime + " before selecting another destination");
        yield return new WaitForSeconds(randomWaitTime);

        //recurse
        walkTask = StartCoroutine(GoRandomPlace(lower, upper, speed));
    }

    //check if player touch the snake
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Body") {
            playerDie();
        }
    }

    public void playerDie() {
        GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LastDeathSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync("DeathScene");
    }
}
