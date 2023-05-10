using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_lastPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject support;
    public static Vector3 lastPosition;
    public static Vector3 lastPositionSupport;
    void Update()
    {
        if(Player_CheckColision.isGround)
        {
            if(Player_Input.OlhandoDireita)
            {lastPosition = new Vector3 (player.transform.position.x -0.3f, player.transform.position.y, player.transform.position.z);
            lastPositionSupport = new Vector3 (support.transform.position.x -0.3f, support.transform.position.y, support.transform.position.z);}
            else {lastPosition = new Vector3 (player.transform.position.x +0.3f, player.transform.position.y, player.transform.position.z);
            lastPositionSupport = new Vector3 (support.transform.position.x +0.3f, support.transform.position.y, support.transform.position.z);}
        }
    }
}
