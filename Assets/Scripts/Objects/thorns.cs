using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class thorns : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private LayerMask Player;
    [SerializeField] private float takeLife;
    private BoxCollider2D boxCol2D;
    private bool isThorns;

    private void Start() 
    {
        boxCol2D = GetComponent<BoxCollider2D>();
    }
    private void Update() 
    {
        // Verficar colisao
        isThorns = Physics2D.IsTouchingLayers(boxCol2D, Player);

        if(isThorns){damage();}
    }

    public void damage()
    {
        // Transicao de camera

        // Teletransportar para ponto de segurança
        if(player_status.isDie == false)
        {
            Player_Physics2D.ResetVelocity();
            Player_Physics2D.playerGameObject.transform.position = player_lastPosition.lastPosition;
        }

        // Tirar ponto de vida
        player_status.reduceLife(takeLife);
    }
}
