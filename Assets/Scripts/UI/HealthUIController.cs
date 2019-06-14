using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    Text healthText;
    PlayerController player;

    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        healthText.text = string.Format(
            "Health: {0} / {1}",
            player.Health.ToString(),
            player.MaxHealth.ToString());
    }
}
