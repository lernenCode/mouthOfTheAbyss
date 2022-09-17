using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallMove : MonoBehaviour
{
    [Header("Wall Sliding")]
    [SerializeField] private float slidVelocity;

    [Header("Wall Jump")]
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private float takeStaminaSliding;
    [SerializeField] private float takeStaminaJump;
    public static bool isJumpWall;
    public static bool isSliding;
    public static bool isJumpWallRight;
    public static bool isJumpWallLeft;
    public static bool isJumpRope;
    private void Update()
    {
        if(isSliding){player_status.reduceStamina(takeStaminaSliding);}
        
        if(Player_Carried.HolderItem == null)
        {
            //> O PERSONAGEM PODE ESCORREGAR NA PAREDE
            if (Player_CheckColision.isWall)
            {
                if (Player_Input.InputRight || Player_Input.InputLeft)
                {
                    isSliding = true;
                }
            }
            else { isSliding = false; }

            //> O PERSONAGEM PODE PULAR DE UM LADO PARA O OUTRO NA PAREDE
            if (Player_Input.InputWallJump)
            {
                // Tirar energia
                player_status.reduceStamina(takeStaminaJump);
                isJumpWall = true;
                if (Player_CheckColision.isWallRight /*&& Player_Input.OlhandoDireita == true*/) { isJumpWallLeft = true; }
                if (Player_CheckColision.isWallLeft /*&& Player_Input.OlhandoDireita == false*/) { isJumpWallRight = true; }
            }

            if(isJumpRope)
            {
                isJumpWall = true;
                if (Player_CheckColision.isWallRight) { isJumpWallLeft = true; }
                if (Player_CheckColision.isWallLeft) { isJumpWallRight = true; }
            }
        }

    }
    private void FixedUpdate()
    {
        //> O PERSONAGEM PODE ESCORREGAR NA PAREDE
        if (isSliding && Player_Carried.HolderItem == null && isJumpWall == false)
        {
            Player_Physics2D.corpoDoPersonagem.velocity = new Vector2(Player_Physics2D.corpoDoPersonagem.velocity.x, Mathf.Clamp(Player_Physics2D.corpoDoPersonagem.velocity.y, -slidVelocity, float.MaxValue));
        }

        //> O PERSONAGEM PODE PULAR DE UM LADO PARA O OUTRO NA PAREDE
        if (isJumpWall == true)
        {
            StartCoroutine(Player_IEnumerator.wallJumpDuration(wallJumpDuration));

            if(isJumpWallRight) { WallJumpRight(); }

            if(isJumpWallLeft) { WallJumpLeft(); }
        }
    }

    private void WallJumpLeft()
    { Player_Physics2D.corpoDoPersonagem.AddForce(new Vector2(-wallJumpForce.x, wallJumpForce.y), ForceMode2D.Impulse);}
    private void WallJumpRight()
    { Player_Physics2D.corpoDoPersonagem.AddForce(new Vector2(wallJumpForce.x, wallJumpForce.y), ForceMode2D.Impulse); }
}
