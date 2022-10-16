using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_lastPosition : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject support;
    public static Vector2 lastPosition;
    public static Vector2 lastPositionSupport;
    void Update()
    {
        if(Player_CheckColision.isGround)
        {
            if(Player_Input.OlhandoDireita)
            {lastPosition = new Vector2 (player.transform.position.x -0.3f, player.transform.position.y);
            lastPositionSupport = new Vector2 (support.transform.position.x -0.3f, support.transform.position.y);}
            else {lastPosition = new Vector2 (player.transform.position.x +0.3f, player.transform.position.y);
            lastPositionSupport = new Vector2 (support.transform.position.x +0.3f, support.transform.position.y);}
        }
    }
}
