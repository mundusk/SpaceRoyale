using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoController : MonoBehaviour
{
    int totalEnemiesThisRound = 0;
    int enemiesDefeatedThisRound = 0;

    public int TotalEnemiesThisRound
    {
        get { return totalEnemiesThisRound; }
        set { totalEnemiesThisRound = value; }
    }

    public int EnemiesDefeatedThisRound
    {
        get { return enemiesDefeatedThisRound; }
        set { enemiesDefeatedThisRound = value; }
    }

    void Update()
    {
        if(enemiesDefeatedThisRound != 0 &&
            enemiesDefeatedThisRound == totalEnemiesThisRound)
        {
            FindObjectOfType<GameManagerController>().RoundCleared();
        }
    }
}
