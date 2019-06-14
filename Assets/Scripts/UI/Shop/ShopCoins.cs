using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCoins : MonoBehaviour
{
    Text currentCoinsText;
    int currentCoins = 0;

    public int CurrentCoins
    {
        get { return currentCoins; }
        set { currentCoins = value; }
    }

    void Start()
    {
        currentCoinsText = GetComponent<Text>();
    }

    void Update()
    {
        currentCoinsText.text = string.Format(
            "Current coins: {0}",
            currentCoins.ToString());
    }
}
