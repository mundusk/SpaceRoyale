using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUIController : MonoBehaviour
{
    Text coinsText;
    GameSessionController gameSession;

    void Start()
    {
        coinsText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSessionController>();
    }

    void Update()
    {
        coinsText.text = string.Format(
            "Coins: {0}",
            gameSession.Coins.ToString());
    }
}
