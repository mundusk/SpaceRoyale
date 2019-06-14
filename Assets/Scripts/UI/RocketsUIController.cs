using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketsUIController : MonoBehaviour
{
    Text rocketsText;
    PlayerController player;

    void Start()
    {
        rocketsText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        rocketsText.text = string.Format(
            "Rockets: {0}",
            player.Rockets.ToString());
    }
}
