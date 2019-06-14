using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUIController : MonoBehaviour
{
    CountdownController countdown;
    Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        countdown = FindObjectOfType<CountdownController>();
        countdownText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int remainingSeconds = (int)countdown.RemainingTime;

        countdownText.text = remainingSeconds.ToString();
    }
}