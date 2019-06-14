using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*The EnemyController object manages all of the Enemy GameObject in game
movements and interactions except for detecting nearby enemies. */
public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject coin;
    
    [Header("Enemy Stats")]
    [SerializeField] float health = 100f;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float distanceToMaintain = 2.5f;
    [SerializeField] float distanceToRetreat = 1f;

    [Header("Projectile Information")]
    [SerializeField] GameObject basicShot;
    [SerializeField] float basicShotVelocity = 14.5f;
    [SerializeField] float fireProjectileWaitTime = 0.25f;

     [Header("Audio")]
    [SerializeField] AudioClip enemyDieAudio;
    [SerializeField] [Range(0,1)] float enemyDieClipVolume = 0.7f;
    [SerializeField] AudioClip enemyBasicShotAudio;
    [SerializeField] [Range(0,1)] float enemyBasicShotClipVolume = 0.2f;

    EnemyInfoController enemyInfo;
    EnemyRadar enemyRadar; //Used to track enemy ships and know when to attack.
    Transform targetEnemy; //The ship to currently focus attacks on.
    Transform targetWaypoint;
    List<Transform> waypointPositions = new List<Transform>();
    float fireProjectileCountdown = 0f;

    void Start()
    {
        enemyInfo = FindObjectOfType<EnemyInfoController>();
        enemyRadar = GetComponentInChildren<EnemyRadar>();
        targetWaypoint = transform;
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        foreach (GameObject waypoint in waypoints)
        {
            waypointPositions.Add(waypoint.transform);
        }
    }

    void Update()
    {
        if(!enemyRadar.EnemyDetected)
        {
            if(WaypointReached())
            {
                FindNextWaypoint();
            } else {
                MoveToWaypoint();
            }
        } else {
            targetEnemy = enemyRadar.IdentifyNearestEnemy();
            
            //TODO: Investigate issue with updating radar when an enemy is destroyed
            //If the player is a certain distance within the radar after the enemy destroys
            //another ship, it will not detect the player ship.
            if(targetEnemy != null)
                Attack();
            else
                enemyRadar.ResetRadar();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Projectile")
        {
            var collidedObject = other.GetComponent<ProjectileController>();
            
            if(gameObject != collidedObject.FiredBy)
            {
                TakeDamage(collidedObject);
                Destroy(other.gameObject);
            }
        }
    }

    private void TakeDamage(ProjectileController collidedObject)
    {
        health -= collidedObject.Damage;

        if(health <= 0)
        {
            Instantiate(
                coin,
                transform.position,
                Quaternion.identity);
            
            //Check if the enemy was destroyed by the player
            //Also make sure a check isn't being performed against a destroyed enemy
            if(collidedObject.FiredBy != null)
            {
                if(collidedObject.FiredBy.tag == "Player")
                {
                    FindObjectOfType<GameSessionController>().Score += 10;
                    FindObjectOfType<GameSessionController>().EnemiesDefeated += 1;
                }
            }
            
            enemyInfo.EnemiesDefeatedThisRound++;

            Die();
        }
    }

    private bool WaypointReached()
    {
        if(transform.position == targetWaypoint.position)
            return true;
        else
            return false;
    }

    private void FindNextWaypoint()
    {
        targetWaypoint = waypointPositions[Random.Range(0, waypointPositions.Count - 1)];
    }

    private void MoveToWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, movementSpeed * Time.deltaTime);
        Vector2 direction = new Vector2(targetWaypoint.position.x - transform.position.x,
            targetWaypoint.position.y - transform.position.y);
        transform.up = direction;
    }

    private void Attack()
    {
        //TODO: Update attack movement pattern to avoid situations where enemy ships just stay still and shoot each other
        //TODO: Potentially let ships attempt to "run" if health gets below a certain threshold
        enemyRadar.DistanceToNearestEnemy = Vector2.Distance(transform.position, targetEnemy.position);

        if(enemyRadar.DistanceToNearestEnemy < distanceToRetreat)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.position, -movementSpeed * Time.deltaTime);
        } else if (enemyRadar.DistanceToNearestEnemy > distanceToMaintain)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                    targetEnemy.position,
                    movementSpeed * Time.deltaTime);
        }

        LookDirection();
        FireProjectile();
    }

    //Adjust up vector to face targetEnemy
    private void LookDirection()
    {
        Vector2 direction = new Vector2(targetEnemy.position.x - transform.position.x,
                targetEnemy.position.y - transform.position.y);

        transform.up = direction;
    }

    private void FireProjectile()
    {
        fireProjectileCountdown -= Time.deltaTime;

        if(fireProjectileCountdown <= 0f)
        {
            GameObject projectile = Instantiate(
                basicShot,
                transform.position,
                transform.rotation) as GameObject;
            projectile.GetComponent<ProjectileController>().FiredBy = this.gameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(
                projectile.transform.up.x * basicShotVelocity,
                projectile.transform.up.y * basicShotVelocity);
            
            AudioSource.PlayClipAtPoint(
                enemyBasicShotAudio,
                transform.position,
                enemyDieClipVolume);
            
            fireProjectileCountdown = fireProjectileWaitTime;
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        AudioSource.PlayClipAtPoint(
            enemyDieAudio,
            transform.position,
            enemyDieClipVolume);
    }
}
