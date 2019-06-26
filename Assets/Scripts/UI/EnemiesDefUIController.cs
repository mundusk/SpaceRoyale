using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesDefUIController : MonoBehaviour
{
    Text defEnemiesText;

    void Start()
    {
        defEnemiesText = GetComponent<Text>();
    }

    void Update()
    {
        defEnemiesText.text = string.Format(
            "Enemies Defeated: {0}",
            GameSessionController.Instance.EnemiesDefeated);
    }
}
