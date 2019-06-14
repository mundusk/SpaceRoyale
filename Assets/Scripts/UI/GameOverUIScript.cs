using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour
{
    Text playerScore;
    int score;
    int finalScore;

    void Start()
    {
        GameObject scoreObject = GameObject.Find("ScoreText");
        playerScore = scoreObject.GetComponent<Text>();
        score = FindObjectOfType<GameSessionController>().Score;
        finalScore = score + (FindObjectOfType<GameSessionController>().Coins * 5);

        playerScore.text = finalScore.ToString();    
    }
}
