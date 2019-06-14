using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerController : MonoBehaviour
{
    void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        int musicManagersAvailable = FindObjectsOfType<MusicManagerController>().Length;

        if(musicManagersAvailable > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}
