using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_lastPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public static Vector3 lastPosition;
    void Update()
    {
        if(Player_CheckColision.isGround && Player_CheckColision.isPlatform == false && Player_CheckColision.isWall == false)
        {
            if(Player_Input.OlhandoDireita)
            {lastPosition = new Vector3 (player.transform.position.x -0.3f, player.transform.position.y, player.transform.position.z);}
            else {lastPosition = new Vector3 (player.transform.position.x +0.3f, player.transform.position.y, player.transform.position.z);}
        }
    }
}
