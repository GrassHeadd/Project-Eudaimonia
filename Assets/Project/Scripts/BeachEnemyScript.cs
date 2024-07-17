using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeachEnemyScript : MonoBehaviour
{
    //--------------------Components--------------------//
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private PoacherDisplayUI poacherDisplayUI;

    //--------------------Coords--------------------//
    [SerializeField]  private float minX = 1200f; 
    [SerializeField] private float maxX = 1224f;
    [SerializeField]  private float minZ = 1790f; 
    [SerializeField] private float maxZ = 1800f;

    //--------------------Rest Time--------------------//
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;

    //--------------------Coroutine--------------------//

    private Coroutine walkTask = null, escapeTask;

    //--------------------Hit Counter--------------------//
    private int hitCounter = -1;

    // Start is called before the first frame update
    
    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        poacherDisplayUI = GameObject.FindGameObjectWithTag("EnemiesLeft").GetComponent<PoacherDisplayUI>();
        walkTask = StartCoroutine(GoRandomPlace(minX, maxX, minZ, maxZ, 1));
        int diffLvl = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LevelIndicator;
        Debug.Log("diff level spawner: " + diffLvl);
        hitCounter = (diffLvl - 3) * 3;
        Debug.Log("hit counter: " + hitCounter);
    }

    //TODO: Movement
    public IEnumerator GoRandomPlace(float lowerX, float upperX, float lowerZ, float upperZ, int speed) {
        float x = UnityEngine.Random.Range(lowerX, upperX), z = UnityEngine.Random.Range(lowerZ, upperZ);

        //Debug.Log("Now pathed to " + x + " and " + z);

        Vector3 newRandomPos = new Vector3(x, transform.position.y, z);
        agent.SetDestination(newRandomPos);

        animator.SetInteger("MoveState", 1);
        animator.speed = 2.0f;

        yield return new WaitForSeconds(2f);

        while (agent.remainingDistance > 0.1f) {yield return new WaitForFixedUpdate();}
        Debug.Log("done running");

        animator.SetInteger("MoveState", 0);

        float randomWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(randomWaitTime);

        walkTask = StartCoroutine(GoRandomPlace(lowerX, upperX, lowerZ, upperZ, speed));
    }

    //TODO: Collider
    public void OnTriggerEnter(Collider other) {
        Debug.Log("triggered collider");
        if(other.tag == "Ammo") {
            Debug.Log("Ammo triggered");
            animator.SetTrigger("Hit");
            hitCounter--;
            poacherDisplayUI.updateCountText(hitCounter);
        }

        if(hitCounter <= 0) {
            //navigate to that specific location
            //poacherDisplayUI updates to minus 1
            animator.SetBool("killed", true);
            runAway();
            poacherDisplayUI.endGame();

        }
    }

    public IEnumerator runAway() {
        Vector3 newPos = new Vector3(1281,13,1810);
        agent.SetDestination(newPos);
        yield return new WaitForSeconds(2f);
        while (agent.remainingDistance > 0.1f) {yield return new WaitForFixedUpdate();}
        yield return new WaitForSeconds(10f);
    }
}
