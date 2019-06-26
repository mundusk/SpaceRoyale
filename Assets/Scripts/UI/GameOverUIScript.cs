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
        score = GameSessionController.Instance.Score;
        finalScore = score + (GameSessionController.Instance.Coins * 5);

        playerScore.text = finalScore.ToString();    
    }
}
