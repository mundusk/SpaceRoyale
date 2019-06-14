using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesDefUIController : MonoBehaviour
{
    Text defEnemiesText;
    GameSessionController gameSession;

    void Start()
    {
        defEnemiesText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSessionController>();
    }

    void Update()
    {
        defEnemiesText.text = string.Format(
            "Enemies Defeated: {0}",
            gameSession.EnemiesDefeated.ToString());
    }
}
