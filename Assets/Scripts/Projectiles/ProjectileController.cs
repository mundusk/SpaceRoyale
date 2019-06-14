using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float lifespan = 5f;
    GameObject firedBy;

    public int Damage {get {return damage;}}

    public GameObject FiredBy
    {
        get { return firedBy; }
        set { firedBy = value; }
    }

    void Update()
    {
        if(lifespan > 0f)
            lifespan -= Time.deltaTime;
        else
            Destroy(gameObject);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}