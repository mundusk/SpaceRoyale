using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUIController : MonoBehaviour
{
    CountdownController countdown;
    Text countdownText;
    Text roundText;

    void Start()
    {
        countdown = FindObjectOfType<CountdownController>();

        GameObject countdownItem = GameObject.Find("CountdownText");
        countdownText = countdownItem.GetComponent<Text>();

        GameObject roundNumItem = GameObject.Find("RoundText");
        roundText = roundNumItem.GetComponent<Text>();
        roundText.text = string.Format(
            "ROUND {0}",
            FindObjectOfType<GameSessionController>().Round.ToString());
    }

    void Update()
    {
        int remainingSeconds = (int)countdown.RemainingTime;

        countdownText.text = remainingSeconds.ToString();
    }
}