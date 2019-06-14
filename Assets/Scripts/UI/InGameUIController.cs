using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    GameObject survivedText;
    GameObject diedText;

    void Awake()
    {
        survivedText = GameObject.Find("SurvivedRoundUI");
        survivedText.gameObject.SetActive(false);

        diedText = GameObject.Find("PlayerDiedUI");
        diedText.gameObject.SetActive(false);
    }
    
    public void SurvivedRound()
    {
        survivedText.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        diedText.gameObject.SetActive(true);
    }
}
