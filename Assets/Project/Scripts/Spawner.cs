using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject theEnemy;
    [SerializeField] private int xMax = 5;
    [SerializeField] private int xMin = -5;
    [SerializeField] private int zMax = 5;
    [SerializeField] private int zMin = -10;
    [SerializeField] private int timeBetwEnemySpawn = 1;
    [SerializeField] private int maxEnemy = 10;
    private int EnemyCount;
    public Vector3 pos;

    private void Start() {
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
            EnemyCount += 1;
        }
    }
}


