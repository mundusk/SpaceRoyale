using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*The EnemyInfoController class keeps track of enemies in a round
and the number of enemies that have been destroyed, not just by the player. */
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
        //A win condition for passing a round is when all enemies are destroyed.
        if(enemiesDefeatedThisRound != 0 &&
            enemiesDefeatedThisRound == totalEnemiesThisRound)
        {
            FindObjectOfType<GameManagerController>().RoundCleared();
        }
    }
}
