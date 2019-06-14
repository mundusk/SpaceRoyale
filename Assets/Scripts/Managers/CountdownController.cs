using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    float remainingTime = 60f;

    public float RemainingTime { get {return remainingTime;}}

    void Update()
    {
        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        } else {
            FindObjectOfType<GameManagerController>().RoundCleared();
        }
    }
}
