using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachEnemyScript : MonoBehaviour
{
    //--------------------Components--------------------//
    private UnityEngine.AI.NavMeshAgent agent;
    //private Animator animator;
    [SerializeField] private GameObject player;
    //[SerializeField] private EnemyDisplayUI enemyDisplayUI; #TODO: add the hits required to chase away UI

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
        //animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        poacherDisplayUI = GameObject.FindGameObjectWithTag("EnemiesLeft").GetComponent<PoacherDisplayUI>();
        walkTask = StartCoroutine(GoRandomPlace(minX, maxX, minZ, maxZ, 1));
        int diffLvl = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LevelIndicator;
        Debug.Log("diff level spawner: " + diffLvl);
        hitCounter = diffLvl - 8;
    }

    //TODO: Movement
    public IEnumerator GoRandomPlace(float lowerX, float upperX, float lowerZ, float upperZ, int speed) {
        float x = UnityEngine.Random.Range(lowerX, upperX), z = UnityEngine.Random.Range(lowerZ, upperZ);

        //Debug.Log("Now pathed to " + x + " and " + z);

        Vector3 newRandomPos = new Vector3(x, transform.position.y, z);
        agent.SetDestination(newRandomPos);

        // animator.SetBool("isRunning", true);
        // animator.speed = 2.0f;

        yield return new WaitForSeconds(2f);

        while (agent.remainingDistance > 0.1f) {yield return new WaitForFixedUpdate();}

        //animator.SetBool("isRunning", false);
        float randomWaitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(randomWaitTime);

        walkTask = StartCoroutine(GoRandomPlace(lowerX, upperX, lowerZ, upperZ, speed));
    }

    //TODO: Collider
    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Ammo") {
            hitCounter--;
        }

        if(hitCounter == 0) {
            //navigate to that specific location
            //poacherDisplayUI updates to minus 1
            runAway();
            poacherDisplayUI.enemiesLeft--;
            gameObject.SetActive(false);

        }
    }

    public void runAway() {
        Vector3 newPos = new Vector3(1281,13,1810);
        agent.SetDestination(newPos);
        yield return new WaitForSeconds(2f);
        while (agent.remainingDistance > 0.1f) {yield return new WaitForFixedUpdate();}
    }
    
}
