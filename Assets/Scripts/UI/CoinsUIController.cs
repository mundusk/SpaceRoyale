using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUIController : MonoBehaviour
{
    Text coinsText;

    void Start()
    {
        coinsText = GetComponent<Text>();
    }

    void Update()
    {
        coinsText.text = string.Format(
            "Coins: {0}",
            GameSessionController.Instance.Coins.ToString());
    }
}
