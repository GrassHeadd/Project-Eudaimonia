using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    private GameObject theEnemy;
    private int xPos;
    private int zPos;
    private int EnemyCount;

    [SerializeField] private int maxEnemy = 20;

    public Vector3 pos;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (EnemyCount < maxEnemy)
        {
            xPos = Random.Range(-5, 5);
            zPos = Random.Range(-10, 5);
            pos = new Vector3 (xPos, 0, zPos);
            NavMeshHit hit;
            NavMesh.SamplePosition(pos, out hit, 2, 1);
            pos = hit.position;
            Instantiate(theEnemy, pos, Quaternion.identity);
            yield return new WaitForSeconds(1);
            EnemyCount += 1;
        }
    }
}

