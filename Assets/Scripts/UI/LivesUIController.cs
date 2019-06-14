using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUIController : MonoBehaviour
{
    Text livesText;
    PlayerController player;

    void Start()
    {
        livesText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        livesText.text = string.Format(
            "Lives: {0}",
            player.Lives.ToString());
    }
}
