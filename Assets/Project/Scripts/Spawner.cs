using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject theEnemy;
    [SerializeField] private int xMax = 5;
    [SerializeField] private int xMin = -5;
    [SerializeField] private int zMax = 5;
    [SerializeField] private int zMin = -10;
    [SerializeField] private float timeBetwEnemySpawn = 1f;
    [SerializeField] private int maxEnemy = 10;
    [SerializeField] private EnemyDisplayUI enemyDisplayUI;

    [SerializeField] private bool shouldUpdateUI = true;
    public int EnemyCount;
    public Vector3 pos;

    public int diffLvl;

    //difficulty setting base on level
    private void Start() {
        diffLvl = GameObject.FindGameObjectWithTag("StaticGameObject").GetComponent<StaticSceneData>().LevelIndicator;
        Debug.Log("diff level spawner: " + diffLvl);
        maxEnemy *= diffLvl;
        
        StartCoroutine(SpawnEnemies());


    }

    private IEnumerator SpawnEnemies() {
        while (EnemyCount < maxEnemy)
        {
            int xCoords = Random.Range(xMin, xMax);
            int zCoords = Random.Range(zMin, zMax);
            pos = new Vector3 (xCoords, 0, zCoords);

            NavMeshHit hit;
            NavMesh.SamplePosition(pos, out hit, 2, 1);
            pos = hit.position;

            Instantiate(theEnemy, pos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetwEnemySpawn);
            EnemyCount++;
            if(shouldUpdateUI) {
                enemyDisplayUI.updateCountText(this.EnemyCount);
            }
            
        }
    }
}


