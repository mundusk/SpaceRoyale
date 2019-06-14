using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    Text scoreText;
    GameSessionController gameSession;

    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSessionController>();    
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format(
            "Score: {0}",
            gameSession.Score);
    }
}
