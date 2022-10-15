using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supprt_Movement : MonoBehaviour
{
    [SerializeField] private float deccelaration;
    [SerializeField] private float acceleration;
    [SerializeField] private float debuffSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float velPower;
    [SerializeField] private float reduceEnergy;
    private float startSpeed;

    void Update()
    {
        #region Movimentação
        // Só pode se mover quando o robo não esta no comando
        if (player_status.isDie == true)
        {
            // Perder energia
            player_status.reduceEnergy(reduceEnergy);

            // Só pode se movimentar se
            if(player_status.energy > 0)
            {
                // Calcular movimento X
                float targetSpeed_X = Support_Physics2D.Direction.x * moveSpeed;
                float speedDif_X = targetSpeed_X - Support_Physics2D.corpoDoPersonagem.velocity.x;
                float accelRate_X = (Mathf.Abs(targetSpeed_X) > 0.01f) ? acceleration : deccelaration;
                float movement_X = Mathf.Pow(Mathf.Abs(speedDif_X) * accelRate_X, velPower) * Mathf.Sign(speedDif_X);

                // Calcular movimento Y
                float targetSpeed_Y = Support_Physics2D.Direction.y * moveSpeed;
                float speedDif_Y = targetSpeed_Y - Support_Physics2D.corpoDoPersonagem.velocity.y;
                float accelRate_Y = (Mathf.Abs(targetSpeed_Y) > 0.01f) ? acceleration : deccelaration;
                float movement_Y = Mathf.Pow(Mathf.Abs(speedDif_Y) * accelRate_Y, velPower) * Mathf.Sign(speedDif_Y);

                // Fazer movimento x
                if (Support_Inputs.InputRight || Support_Inputs.InputLeft)
                {
                    Support_Physics2D.corpoDoPersonagem.AddForce(movement_X * Vector2.right);
                }
                else { Support_Physics2D.corpoDoPersonagem.velocity = new Vector2(0, Support_Physics2D.corpoDoPersonagem.velocity.y); }

                // Fazer movimento y
                if (Support_Inputs.InputUp || Support_Inputs.InputDown)
                {
                    Support_Physics2D.corpoDoPersonagem.AddForce(movement_Y * Vector2.up);
                }
                else { Support_Physics2D.corpoDoPersonagem.velocity = new Vector2(Support_Physics2D.corpoDoPersonagem.velocity.x, 0); }
            } else {Support_Physics2D.ResetVelocity();}
        }
        #endregion

    }
}
