using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_lastPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public static Vector2 lastPosition;
    void Update()
    {
        if(Player_CheckColision.isGround)
        {
            if(Player_Input.OlhandoDireita){lastPosition = new Vector2 (player.transform.position.x -0.5f,player.transform.position.y);}
            else {lastPosition = new Vector2 (player.transform.position.x +0.5f,player.transform.position.y);}
        }
    }
}
