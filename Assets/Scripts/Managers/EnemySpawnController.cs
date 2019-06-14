using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float timeBetweenSpawns = 0.5f;

    EnemyInfoController enemyInfo;
    List<Transform> enemySpawners = new List<Transform>();
    float spawnWaitTime;
    int numOfEnemiesThisRound = 1;
    int enemiesSpawned = 0;

    void Start()
    {
        enemyInfo = FindObjectOfType<EnemyInfoController>();
        spawnWaitTime = timeBetweenSpawns;
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");

        foreach(GameObject spawner in spawners)
        {
            enemySpawners.Add(spawner.transform);
        }

        CalculateNumberOfEnemies();    
    }

    void Update()
    {
        if(enemiesSpawned < numOfEnemiesThisRound)
        {
            if(spawnWaitTime <= 0)
            {
                SpawnEnemy();
                enemiesSpawned++;
            }
            else
                spawnWaitTime -= Time.deltaTime;
        }
    }
    
    private void CalculateNumberOfEnemies()
    {
        numOfEnemiesThisRound = 2 * FindObjectOfType<GameSessionController>().Round + 2;
        enemyInfo.TotalEnemiesThisRound = numOfEnemiesThisRound;
    }

    //TODO: Figure out bug that sometimes causes "extra" enemies to be spawned
    //Also a possibility that enemies are sometimes not being destroyed.
    private void SpawnEnemy()
    {
        int randomPoint = Random.Range(0, enemySpawners.Count - 1);

        Instantiate(
            enemy,
            enemySpawners[randomPoint].transform.position,
            Quaternion.identity);
        
        spawnWaitTime = timeBetweenSpawns;
    }
}
