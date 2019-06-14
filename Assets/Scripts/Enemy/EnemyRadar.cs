using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*The EnemyRadar class functions like a radar and keeps track of nearby ships. */
public class EnemyRadar : MonoBehaviour
{
    bool enemyDetected = false;

    List<GameObject> enemiesWithinRadar = new List<GameObject>();
    Transform nearestEnemy;
    float distanceToNearestEnemy = 20f;

    void Update()
    {
        if(enemiesWithinRadar.Count == 0)
            ResetRadar();
    }

    public bool EnemyDetected { get { return enemyDetected; }}
    public float DistanceToNearestEnemy
    {
        get { return distanceToNearestEnemy;}
        set { distanceToNearestEnemy = value;}
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Player")
        {
            if(!enemiesWithinRadar.Contains(other.gameObject))
                enemiesWithinRadar.Add(other.gameObject);
            
            enemyDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Player")
        {
            if(enemiesWithinRadar.Contains(other.gameObject))
                enemiesWithinRadar.Remove(other.gameObject);
        }
    }

    public void ResetRadar()
    {
        enemyDetected = false;
        nearestEnemy = null;
        distanceToNearestEnemy = 20f;
    }

    public Transform IdentifyNearestEnemy()
    {
        float enemyDistance;

        for(int i = 0; i < enemiesWithinRadar.Count; i++)
        {
            enemyDistance = Vector2.Distance(transform.position, enemiesWithinRadar[i].transform.position);

            if(enemyDistance < distanceToNearestEnemy)
            {
                nearestEnemy = enemiesWithinRadar[i].transform;
            }
        }

        return nearestEnemy;
    }
}