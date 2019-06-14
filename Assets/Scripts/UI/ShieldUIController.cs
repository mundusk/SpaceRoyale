using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUIController : MonoBehaviour
{
    Text shieldText;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        shieldText = GetComponent<Text>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        shieldText.text = string.Format(
            "Shield: {0} / {1}",
            player.Shield.ToString(),
            player.MaxShield.ToString());
    }
}
