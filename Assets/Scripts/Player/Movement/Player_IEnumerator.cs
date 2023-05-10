using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IEnumerator : MonoBehaviour
{
    #region "Dash"
    public static IEnumerator durationDash(float dashDuration) // Duracao de dash⌚
    {
        Player_Physics2D.ResetVelocity();
        Player_Input.canMove = false;
        yield return new WaitForSeconds(dashDuration);
        Player_Input.canMove = true;
        Player_Dash.runningDash = false;
        Player_Dash.isDashing = false;
        Player_Physics2D.ResetVelocity();
        Player_Dash.dashInCooldown = true;
    }

    public static IEnumerator cooldownDash(float dashCooldown) // cooldown de Dash⌚
    {
        yield return new WaitForSeconds(dashCooldown);
        if (Player_CheckColision.isGround || Player_CheckColision.isWall || Player_CheckColision.isPlatform)
        {
            Player_Dash.dashInCooldown = false;
        }
    }
    #endregion

    #region carry
    public static IEnumerator carryLock()
    {
        Player_Carried.carryLock = true;
        yield return new WaitForSeconds(0.2f);
        Player_Carried.carryLock = false;
    }
    #endregion

    #region wallJump
    public static IEnumerator wallJumpDuration(float JumpDuration)
    {
        Player_Physics2D.ResetVelocity();
        Player_Input.canMove = false;
        yield return new WaitForSeconds(JumpDuration);
        Player_Input.canMove = true;
        Player_WallMove.isJumpWallRight = false;
        Player_WallMove.isJumpWallLeft = false;
        Player_WallMove.isJumpWall = false;
        if (Player_WallMove.isJumpRope)
        {
            Player_WallMove.isJumpRope = false;
        }
    }
    #endregion

    #region canDamageTimer
    public static IEnumerator canDamageTime()
    {
        player_status.recovery = true;
        yield return new WaitForSeconds(0.1f);
        player_status.recovery = false;
    }
    #endregion
}
