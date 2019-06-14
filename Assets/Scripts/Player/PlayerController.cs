using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject basicShotObject;
    [SerializeField] GameObject rocketObject;
    [SerializeField] int health = 100;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int shield = 40;
    [SerializeField] int maxShield = 40;
    [SerializeField] int lives = 3;
    [SerializeField] int rockets = 3;
    [SerializeField] int maxRockets = 3;
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float basicShotVelocity = 14.5f;
    [SerializeField] float rocketVelocity = 20f;
    [SerializeField] float rapidFireWaitDuration = 0.15f;

    Coroutine rapidFireCoroutine;
    GameSessionController gameSession;
    float shieldRechargeWaitTime = 10f;
    float timeSinceDamageTaken = 0f;

    public int Health {get {return health;}}
    public int MaxHealth {get {return maxHealth;}}
    public int Shield {get {return shield;}}
    public int MaxShield {get {return maxShield;}}
    public int Lives {get {return lives;}}
    public int Rockets {get {return rockets;}}

    void Awake()
    {
        GetCurrentPlayerStats();
    }

    void Update()
    {
        timeSinceDamageTaken += Time.deltaTime;

        Move();
        LookDirection();
        FireProjectile();
        RechargeShield();
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
                timeSinceDamageTaken = 0f;
            }
        }

        if(other.transform.tag == "Coin")
        {
            gameSession.Coins += 1;

            Destroy(other.gameObject);
        }
    }

    private void GetCurrentPlayerStats()
    {
        gameSession = FindObjectOfType<GameSessionController>();

        lives = gameSession.Lives;
        shieldRechargeWaitTime = gameSession.ShieldRechargeTime;
        maxShield = gameSession.Shield;
        shield = maxShield;
        rockets = gameSession.Rockets;
        maxRockets = gameSession.MaxRockets;
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float deltaY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        //TODO: Implement a cleaner way to keep player within game boundaries
        float newXPos = transform.position.x;
        float newYPos = transform.position.y;
        
        //Check if the new player position will exceed the world boundary
        if(deltaX < 0)
        {
            if((transform.position.x + deltaX) < -28f)
                newXPos = -28f;
            else
                newXPos = transform.position.x + deltaX;
        } else if(deltaX > 0)
        {
            if((transform.position.x + deltaX) > 28f)
                newXPos = 28f;
            else
                newXPos = transform.position.x + deltaX;
        }

        //TODO: FIX THIS!!! Bounding the Y axis isn't working
        if(deltaY < 0)
        {
            if((transform.position.y + deltaY) < 5f)
                newYPos = 5f;
            else
                newYPos = transform.position.y + deltaY;
        } else if(deltaY > 0)
        {
            if((transform.position.y + deltaY) > -5f)
                newYPos = -5f;
            else
                newYPos = transform.position.y + deltaY;
        }

        newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void LookDirection ()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);
        
        transform.up = direction;
    }

    private void FireProjectile()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            rapidFireCoroutine = StartCoroutine("RapidFire");
        } else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(rapidFireCoroutine);
        }

        if(Input.GetButtonDown("Fire2"))
        {
            if(rockets > 0)
            {
                FireRocket();
                rockets--;
            }
        }
    }

    private void TakeDamage(ProjectileController collidedObject)
    {
        if(shield >= collidedObject.Damage)
        {
            shield -= collidedObject.Damage;
        } else if(shield > 0 && shield < collidedObject.Damage)
        {
            int carryoverDamage = collidedObject.Damage - shield;
            health -= carryoverDamage;
        } else {
            health -= collidedObject.Damage;
        }

        if(health <= 0)
        {
            if(lives > 1)
            {
                LostALife();
            } else {
                health = 0;
                FindObjectOfType<GameManagerController>().PlayerDied();
                Destroy(gameObject);
            }
        }
    }

    private void RechargeShield()
    {
        if(shield < maxShield && timeSinceDamageTaken >= 10f)
        {
            shield += 1;
        }
    }

    private void LostALife()
    {
        lives--;
        health = maxHealth;
        shield = maxShield;

        transform.position = Vector2.zero;
    }

    private void FireRocket()
    {
        InstantiateProjectile("rocket");
    }

    IEnumerator RapidFire()
    {
        while(true)
        {
            InstantiateProjectile("basicShot");
            
            yield return new WaitForSeconds(rapidFireWaitDuration);
        }
    }

    private void InstantiateProjectile(string projectile)
    {
        if(projectile == "basicShot")
        {
            GameObject basicShot = Instantiate(
                basicShotObject,
                transform.position,
                transform.rotation) as GameObject;
            basicShot.GetComponent<ProjectileController>().FiredBy = this.gameObject;
            basicShot.GetComponent<Rigidbody2D>().velocity = new Vector2(
                basicShot.transform.up.x * basicShotVelocity,
                basicShot.transform.up.y * basicShotVelocity);
        } else if (projectile == "rocket")
        {
            GameObject rocket = Instantiate(
                rocketObject,
                transform.position,
                transform.rotation) as GameObject;
            rocket.GetComponent<ProjectileController>().FiredBy = this.gameObject;
            rocket.GetComponent<Rigidbody2D>().velocity = new Vector2(
                rocket.transform.up.x * rocketVelocity,
                rocket.transform.up.y * rocketVelocity);
        }
    }

    public void SavePlayerStats()
    {
        gameSession.Lives = lives;
        gameSession.Rockets = rockets;
    }
}
