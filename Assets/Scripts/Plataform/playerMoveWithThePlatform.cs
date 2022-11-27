using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveWithThePlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if(col.gameObject.CompareTag("platform"))
        {Player_Physics2D.playerGameObject.transform.parent = col.transform;}
    }

    private void OnCollisionExit2D(Collision2D col) 
    {
        if(col.gameObject.CompareTag("platform"))
        {Player_Physics2D.playerGameObject.transform.parent = null;}
    }
}
