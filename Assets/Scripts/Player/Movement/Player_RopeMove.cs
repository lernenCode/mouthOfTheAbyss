using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RopeMove : MonoBehaviour
{
    [Header("GraplingRope")]
    [SerializeField] private float Speed;
    public static bool GraplingRope;
    private void FixedUpdate()
    {
        #region movimento
        // Grabling
        if (RopeDraw.RopeInColision)
        {
            
            if (RopeDraw.ropeUp)
            {
                // Garantir forças de fisica 🌠
                Player_Physics2D.ResetVelocity();

                // Falar que estou fazendo o Dash 0️⃣|1️⃣
                GraplingRope = true;

                // Executar Dash 🏃
                Player_Physics2D.corpoDoPersonagem.AddForce
                (new Vector2(0, Speed), ForceMode2D.Impulse);
            }

            else if (Player_Input.OlhandoDireita)
            {
                // Garantir forças de fisica 🌠
                Player_Physics2D.ResetVelocity();

                // Falar que estou fazendo o Dash 0️⃣|1️⃣
                GraplingRope = true;

                // Executar Dash 🏃
                Player_Physics2D.corpoDoPersonagem.AddForce
                (new Vector2(Speed, 0), ForceMode2D.Impulse);
            }

            else
            {
                // Garantir forças de fisica 🌠
                Player_Physics2D.ResetVelocity();

                // Falar que estou fazendo o Dash 0️⃣|1️⃣
                GraplingRope = true;

                // Executar Dash 🏃
                Player_Physics2D.corpoDoPersonagem.AddForce
                (new Vector2(-Speed, 0), ForceMode2D.Impulse);
            }
        }

        // baque da parede
        if (GraplingRope)
        {
            if (Player_CheckColision.isWall || Player_CheckColision.isPlatformLeft || Player_CheckColision.isPlatformRight || Player_CheckColision.isRoof)
            {
                GraplingRope = false;
                Player_WallMove.isJumpRope = true;
            }
        }

        // StopGravity e movement
        if (RopeDraw.InputRope && RopeDraw.RopeInColision == false)
        {
            Player_Physics2D.ResetVelocity();
            Player_Physics2D.corpoDoPersonagem.gravityScale = 0;
            Player_Physics2D.corpoDoPersonagem.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
        }
        #endregion
    }
}
