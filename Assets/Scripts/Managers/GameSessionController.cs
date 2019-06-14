using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionController : MonoBehaviour
{
    //public static GameSessionController Instance { get; private set;}

    int round = 1;
    int lives = 3;
    int rockets = 3;
    int maxRockets = 3;
    int shield = 40;
    float shieldRechargeTime = 10f;
    int score = 0;
    int coins = 0;
    int enemiesDefeated = 0;

    public int Round
    {
        get { return round; }
        set { round = value; }
    }

    public int Lives
    {
        get { return lives;}
        set { lives = value;}
    }

    public int Rockets
    {
        get { return rockets;}
        set { rockets = value;}
    }

    public int MaxRockets
    {
        get { return maxRockets;}
        set { maxRockets = value;}
    }

    public int Shield
    {
        get { return shield;}
        set { shield = value;}
    }

    public float ShieldRechargeTime
    {
        get { return shieldRechargeTime; }
        set { shieldRechargeTime = value; }
    }

    public int Score
    {
        get { return score;}
        set { score = value;}
    }

    public int Coins
    {
        get { return coins;}
        set { coins = value;}
    }

    public int EnemiesDefeated
    {
        get { return enemiesDefeated; }
        set { enemiesDefeated = value; }
    }

    void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        int gameSessionsAvailable = FindObjectsOfType<GameSessionController>().Length;

        if(gameSessionsAvailable > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void ResetValues()
    {
        round = 1;
        lives = 3;
        rockets = 3;
        maxRockets = 3;
        shield = 40;
        shieldRechargeTime = 10f;
        score = 0;
        coins = 0;
        enemiesDefeated = 0;
    }
}
