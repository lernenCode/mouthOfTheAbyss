using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BetterJump : MonoBehaviour
{
    [Header("BetterJump")]
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowMultiplier;
    private void FixedUpdate()
    {
        // Só posso fazer isso se:
        if (RopeDraw.InputRope == false && Player_WallMove.isJumpWall == false)
        {
            if (Player_Physics2D.corpoDoPersonagem.velocity.y < 0)
            {
                Player_Physics2D.corpoDoPersonagem.gravityScale = fallMultiplier;
            }
            else if (Player_Physics2D.corpoDoPersonagem.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                Player_Physics2D.corpoDoPersonagem.gravityScale = lowMultiplier;
            }
            else { Player_Physics2D.corpoDoPersonagem.gravityScale = 1f; }
        }
    }
}
