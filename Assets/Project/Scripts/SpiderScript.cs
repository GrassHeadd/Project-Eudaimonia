using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpiderScript : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject escapeObj;
    [SerializeField] private float scareDistance = 1.5f;
    //grabbing stuff for grabbing
    [SerializeField] private bool isGrabbed = false, isPlayerNear = false;

    //-------------------coords-------------------//
    [SerializeField]  private float minCoord = -4.5f; 
    [SerializeField] private float maxCoord = 4.5f;
    [SerializeField] private float minWaitTime = 3f;
    [SerializeField] private float maxWaitTime = 7f;
    [SerializeField] private EnemyDisplayUI enemyDisplayUI;
    private Coroutine walkTask = null, escapeTask;

    void Start()
    {
        //initialising to assign the components from unity inspector
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        enemyDisplayUI = GameObject.FindGameObjectWithTag("SpidersLeft").GetComponent<EnemyDisplayUI>();
        //starting the walking inside the coordinates at an animation walking speed of 1
        walkTask = StartCoroutine(GoRandomPlace(minCoord, maxCoord, 1));
    }

    void FixedUpdate()
    {
        // Constantly check 50 times a second to see if player is nearby. if it is, trigger an escape task.
        if (!isGrabbed && Vector3.Distance(player.transform.position, transform.position) < scareDistance) {
            // Debug.Log("Player is near");
            if (escapeTask == null) escapeTask = StartCoroutine(RunFromPlayerTask());
        }
    }


    /// <summary>
    /// makes the spider roam around,
    /// takes in upper and lower coordinates for you to limit the range of spider and speed of animation settings(0,1,2)
    /// returns the runnable to execute it
    /// </summary>
    public IEnumerator GoRandomPlace(float lower, float upper, int speed)
    {
        //Debug.Log("Walking normally === ");
        //generate a random max and min coordinate
        float x = UnityEngine.Random.Range(lower, upper), z = UnityEngine.Random.Range(lower, upper);

        //Debug.Log("Now pathed to " + x + " and " + z);

        //generate coordinates for a new random position
        Vector3 newRandomPos = new Vector3(x, transform.position.y, z);
        //set the destination of travel to the new position
        agent.SetDestination(newRandomPos);
        //teleporting the marker to where the spider is moving towards(just to see where the spider is going for debugging)
        
        if (escapeObj != null) escapeObj.transform.position = newRandomPos;

        //set the animation move speed
        animator.SetBool("isRunning", true);
        animator.speed = 2.0f;

        //waiting for calculations of the new coordinates so it starts walking
        yield return new WaitForSeconds(2f);

        // Has spider reached? If not, keep pausing the code here
        while (agent.remainingDistance > 0.1f)
        {
            //Debug.Log("WAITING");
            yield return new WaitForFixedUpdate();
        }

        //Debug.Log("Stopped " + agent.remainingDistance);
        // Spider Reached! Set to Idle and wait random seconds then do another random movement
        animator.SetBool("isRunning", false);
        float randomWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        //Debug.Log("Reached! Now waiting for " + randomWaitTime + " before selecting another destination");
        yield return new WaitForSeconds(randomWaitTime);

        //recurse
        walkTask = StartCoroutine(GoRandomPlace(lower, upper, speed));
    }



    //javadoc later cus im lazy lmao
    public IEnumerator RunFromPlayerTask() {
        // Debug.Log("Running!");
        //stop the current task of the spider aka walking
        StopCoroutine(walkTask);
        // Logic for escaping, anywhere between 5 to 10 meters
        float escapeRange = Random.Range(5, 10);
        // finds a random point inside the sphere
        Vector3 randomPointInSphere = Random.insideUnitSphere * escapeRange;

        //translate the random point to navmeshing(finds the closest point in navmesh that fits the rando point in sphere)
        randomPointInSphere += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPointInSphere, out hit, escapeRange, 1);
        Vector3 finalPosition = hit.position;
        if (escapeObj != null) escapeObj.transform.position = finalPosition;
        //set the destination
        agent.SetDestination(finalPosition);
        
        //set the animation move speed
        animator.SetBool("isRunning", true);

        // See if spider has reached the escape destination
        yield return new WaitForSeconds(2f);

        // Has spider reached? If not, keep pausing the code here
        while (agent.remainingDistance > 0.1f) yield return new WaitForFixedUpdate();

        // Spider reached! Is player still near it? (i.e. still chasing it the whole while)
        if (isPlayerNear)
        {
            escapeTask = StartCoroutine(RunFromPlayerTask());
            // Debug.Log("Escape failed, trying to run again...");
        }
        // Player no longer chasing it! Resume normal walking tasks
        else
        {
            // Debug.Log("Escape success!");
            escapeTask = null;
            walkTask = StartCoroutine(GoRandomPlace(minCoord, maxCoord, 1));
        }
    }

    //javac doc to-do but just stopping the task
    public void StopPathing() {
        StopCoroutine(walkTask);
        agent.isStopped = true;
        isGrabbed = true;
    }

    //javac doc to-do but just restarting the task
    public void StartPathingAgain() {
        isGrabbed = false;
        //drop it to y=0;
        transform.position.Set(transform.position.x, 0, transform.position.z);
        agent.isStopped = true;
        agent.ResetPath();
        walkTask = StartCoroutine(GoRandomPlace(minCoord, maxCoord, 1));
        
    }

    public void spidersLeft() {
        GameObject spiderSpawner = GameObject.FindGameObjectWithTag("Spawner");
        //Debug.Log("Object found: " + spiderSpawner);
        Spawner spawner = spiderSpawner.GetComponent<Spawner>();

        spawner.EnemyCount--;
        enemyDisplayUI.updateCountText(spawner.EnemyCount);

        if(spawner.EnemyCount <= 0) {
            enemyDisplayUI.endGame();
        }
    }
}










