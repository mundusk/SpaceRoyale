using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIController : MonoBehaviour
{
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();  
    }

    void Update()
    {
        scoreText.text = string.Format(
            "Score: {0}",
            GameSessionController.Instance.Score);
    }
}
