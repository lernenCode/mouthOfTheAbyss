using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float deccelaration;
    [SerializeField] private float acceleration;
    [SerializeField] private float debuffSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float velPower;

    private void FixedUpdate()
    {
        // Se nao tiver fazendo nenhum outro movimento
        if (Player_Dash.runningDash == false && Player_Rope.drawingRope == false
        && Player_WallMove.isJumpWall == false)
        {
            // Movimento
            if (Player_Input.InputRight || Player_Input.InputLeft)
            {
                if (Player_Carried.HolderItem != null)
                { run(debuffSpeed); }
                else { run(moveSpeed); }
            }

            // Parar movimento
            else if (Player_CheckColision.isGround || Player_CheckColision.isPlatformGrounded)
            {
                Player_Physics2D.corpoDoPersonagem.velocity = new Vector2(0, Player_Physics2D.corpoDoPersonagem.velocity.y);
            }
        }
    }

    private void run(float moveSpeed)
    {
        // Calcular movimento
        float targetSpeed = Player_Physics2D.Direction.x * moveSpeed;
        float speedDif = targetSpeed - Player_Physics2D.corpoDoPersonagem.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deccelaration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        Player_Physics2D.corpoDoPersonagem.AddForce(movement * Vector2.right);

    }
}