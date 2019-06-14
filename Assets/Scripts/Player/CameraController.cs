using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

     private Vector3 offset;
     
    void Start () 
    {
        //Offset value to ensure the camera stays in front of the player on the z axis
        offset = new Vector3(0, 0, -10);
    }

    void LateUpdate () 
    {
        if(player != null)
            transform.position = player.transform.position + offset;
    }
}
