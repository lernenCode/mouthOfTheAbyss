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
    private float startSpeed;

    private void Start() { startSpeed = moveSpeed; }
    private void Update()
    {
        if (Player_Carried.HolderItem != null)
        {
            moveSpeed = debuffSpeed;
            acceleration = debuffSpeed;
        }
        else { moveSpeed = startSpeed; acceleration = startSpeed; }
    }
    private void FixedUpdate() // Uma vez que cada segundo | Operações relacionadas à física | Consistente
    {
        #region MoveHorizontal
            // Calcular movimento
            float targetSpeed = Player_Physics2D.Direction.x * moveSpeed;
            float speedDif = targetSpeed - Player_Physics2D.corpoDoPersonagem.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deccelaration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            // Fazer movimento
            if (Player_Input.InputRight || Player_Input.InputLeft)
            {
                if (Player_Dash.runningDash == false && RopeDraw.InputRope == false && Player_WallMove.isJumpWall == false && player_status.isDie == false)
                {
                    Player_Physics2D.corpoDoPersonagem.AddForce(movement * Vector2.right);
                }
            }

            // Parar movimento
            else if (Player_Dash.runningDash == false && RopeDraw.InputRope == false && Player_CheckColision.isGround == true && Player_WallMove.isJumpWall == false)
            {
                Player_Physics2D.corpoDoPersonagem.velocity = new Vector2(0, Player_Physics2D.corpoDoPersonagem.velocity.y);
            }
        #endregion
    }
}
